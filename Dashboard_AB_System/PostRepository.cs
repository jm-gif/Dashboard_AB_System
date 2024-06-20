using System.Data;
using System.Data.SqlClient;

public class PostRepository
{
    private readonly string connectionString;

    public PostRepository()
    {
        connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PostDBEntities"].ConnectionString;
    }

    public DataTable GetAllPosts()
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Id, Image, Description FROM Posts";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(dataTable);
        }
        return dataTable;
    }

    public void CreatePost(byte[] image, string description)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Posts (Image, Description) VALUES (@Image, @Description)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Image", image);
            command.Parameters.AddWithValue("@Description", description);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void DeletePost(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Posts WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
