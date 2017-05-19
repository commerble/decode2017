using decodedon.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace decodedon
{
    public class TootLocal : ITootAccessor
    {
        public bool Add(Toot toot)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Execute("INSERT Toots (Name,Text,CreateAt) VALUES(@name,@text,@createAt)",
                    new
                    {
                        name = toot.Name,
                        text = toot.Text,
                        createAt = toot.CreateAt,
                    }) == 1;
            }
        }

        // c# 6 features : expression bodies - get only property
        string ConnectionString => ConfigurationManager.ConnectionStrings["decodedon"].ConnectionString;

        public IEnumerable<Toot> Load(int take, int skipToken = 0)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var toots = connection.Query<Toot>("SELECT TOP (@take) * FROM Toots WHERE Id > @skipToken Order By CreateAt DESC", new { take, skipToken });
                return toots;
            }
        }
    }
}
