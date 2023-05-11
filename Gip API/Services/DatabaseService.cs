using System.Data.OleDb;
using System.Security.Cryptography.X509Certificates;
using Gip_API.Interfaces;

namespace Gip_API.Services;

public class DatabaseService
{
    //Connectionstring Desktop Thuis
    //private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source= C:\Users\Christophe\Downloads\DataBase GIP.accdb; Persist Security Info=False;";
    
    //Connectionstring Laptop School
    private const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source= C:\Users\ChristopheVanderHeyd\Desktop\DataBase GIP.accdb; Persist Security Info=False;";

    private readonly OleDbConnection connection = new OleDbConnection();
    private OleDbCommand command;
    private OleDbDataReader reader;
    private List<Component> components;
    private string query;


    public DatabaseService()
    {
        //TODO create connection and set connection
        connection = new OleDbConnection(ConnectionString);
        command = new OleDbCommand();
        command.Connection = connection;
    }

    private void OpenConnection()
    {
        connection.Open();
    }

    public void CloseConnection()
    {
        connection.Close();
    }

    public List<Component> GetAllComponents()
    {
        //query to get all components
        OpenConnection();

        query = "SELECT Component, Aantal, Pad FROM InhoudMagazijn ORDER BY Component ASC;";

        command = new OleDbCommand(query, connection);

        components = new List<Component>();

        try
        {
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                components.Add(new Component
                {
                    ComponentName = reader.GetString(0),
                    Amount = reader.GetInt32(1),
                    Path = ConvertColor.ConvertStringToColor(reader.GetString(2))
                });
            }
            return components;
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            CloseConnection();
        }

    }
    public bool AddComponent(Component component)
    {
        string componentToevoegen = "";
        int aantal = 0;
        bool present = false;

        //retrieve data check on if component is already in database if not add component else add amount to component
        try
        {
            OpenConnection();

            query = $"SELECT Component, Aantal FROM InhoudMagazijn WHERE Component = componentName";
            command = new OleDbCommand(query, connection);

            command.Parameters.AddWithValue("@componentName", component.ComponentName);

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                componentToevoegen = reader.GetString(0);
                aantal = reader.GetInt32(1);
            }
            if (componentToevoegen != "")
            {
                present = true;
            }
        }
        catch (Exception)
        {
            present = false;
        }
        finally
        {
            CloseConnection();
        }

        if (present)
        {
            query = $"UPDATE InhoudMagazijn SET Aantal = amount WHERE Component = componentName;";

            try
            {
                OpenConnection();

                command = new OleDbCommand(query, connection);

                command.Parameters.AddWithValue("@amount", (aantal + component.Amount));
                command.Parameters.AddWithValue("@componentName", component.ComponentName);

                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        else
        {


            try
            {
                OpenConnection();

                query = $"INSERT INTO InhoudMagazijn([Component], [Aantal], [Pad]) VALUES (componentName, amount, path);";

                command = new OleDbCommand(query, connection);

                command.Parameters.AddWithValue("@componentName", component.ComponentName);
                command.Parameters.AddWithValue("@amount", component.Amount);
                command.Parameters.AddWithValue("@path", component.Path);

                command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
    public bool DeleteComponent(Component component)
    {
        //create query to add component

        try
        {
            OpenConnection();

            query = $"DELETE FROM InhoudMagazijn WHERE Component = ComponentName";

            command = new OleDbCommand(query, connection);

            command.Parameters.AddWithValue("@ComponentName", component.ComponentName);

            command.ExecuteNonQuery();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }
    public bool UpdateComponent(Component component)
    {
        //update component

        int aantal = 0;

        try
        {
            OpenConnection();

            query = $"SELECT Aantal FROM InhoudMagazijn WHERE Component = componentName;";

            command = new OleDbCommand(query, connection);

            command.Parameters.AddWithValue("@componentName", component.ComponentName);

            reader = command.ExecuteReader();
        }
        catch
        {
            CloseConnection();
        }
        try
        {
            while (reader.Read())
            {
                aantal = reader.GetInt32(0);
            }
            if (aantal != 0 && aantal >= component.Amount)
            {
                query = $"UPDATE InhoudMagazijn SET Aantal = {aantal - component.Amount} WHERE Component = '{component.ComponentName}';";
                command = new OleDbCommand(query, connection);
                
                command.ExecuteNonQuery();

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
        finally
        {
            CloseConnection();
        }
    }
    public List<Component> AlertComponents()
    {
        OpenConnection();

        query = "SELECT Component, Aantal, Pad FROM InhoudMagazijn WHERE Aantal < 11";
        
        command = new OleDbCommand(query, connection);

        components = new List<Component>();

        try
        {
            reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                components.Add(new Component
                {
                    ComponentName = reader.GetString(0),
                    Amount = reader.GetInt32(1),
                    Path = ConvertColor.ConvertStringToColor(reader.GetString(2))
                });
            }
            return components;
        }
        catch
        {
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }
}
