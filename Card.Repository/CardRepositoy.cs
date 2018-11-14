using Card.Helper.ExceptionLog;
using Card.Interface.IRepo;
using Card.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Repository
{
    public class CardRepositoy : ICardRepositoy
    {
        private CardContext context;
        public CardRepositoy(CardContext context)
        {
            this.context = context;
        }

        public IEnumerable<string> ValidateCardWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<string>(query, parameters);
        }
        public int InsertUpdate(CardNumber cardNumber)
        {
            try
            {
                var value = 0;
                using (var context = new CardContext())
                {
                    string ConnectionString = context.Database.Connection.ConnectionString;
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectionString);
                    SqlConnection con = new SqlConnection(builder.ConnectionString);
                    con.Open();
                    var cmd =
                         new SqlCommand("InsertOrUpdateCardNumber", connection: con)
                         {
                             CommandType = CommandType.StoredProcedure
                         };
                    cmd.Parameters.AddWithValue("@Id", cardNumber.Id);
                    cmd.Parameters.AddWithValue("@CardTypeId", cardNumber.CardTypeId);
                    cmd.Parameters.AddWithValue("@CNumber", cardNumber.CNumber);
                    cmd.Parameters.AddWithValue("@Expiry", cardNumber.Expiry);
                    cmd.Parameters.AddWithValue("@Active", cardNumber.Active);
                    cmd.Parameters.AddWithValue("@Editor", cardNumber.Editor);
                    cmd.Parameters.AddWithValue("@Author", cardNumber.Author);
                    cmd.Parameters.AddWithValue("@Created", cardNumber.Created);
                    cmd.Parameters.AddWithValue("@Modified", cardNumber.Modified);
                    value = cmd.ExecuteNonQuery();
                    con.Close();
                    return value;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle.PrintException(ex);
                throw;
            }

        }
    }
}
