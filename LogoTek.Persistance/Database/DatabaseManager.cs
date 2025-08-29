using Dapper;
using LogoTek.Application.Infrastructure.DatabaseManager;
using LogoTek.Domain.Models;
using Microsoft.Data.SqlClient;

namespace LogoTek.Persistance.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly string? _sqlConnection;

        public DatabaseManager(string? sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task CreateTableAsync()
        {
            const string query = """
                CREATE TABLE [dbo].[Telegrams](
                	[PROCESS] [char](4) NOT NULL,
                	[SEQNUM] [smallint] NOT NULL,
                	[TELDT] [smalldatetime] NOT NULL,
                	[TELTYPE] [char](10) NOT NULL,
                	[TELLEN] [smallint] NOT NULL,
                	[IDSNDR] [char](4) NOT NULL,
                	[IDRCVR] [char](4) NOT NULL,
                	[STATUS] [bit] NOT NULL,
                	[PAYLOAD] [binary](128) NOT NULL,
                	[PKID] [int] IDENTITY(1,1) NOT NULL,
                 CONSTRAINT [PK_Telegrams] PRIMARY KEY CLUSTERED ( [PKID] ASC )
                ) ON [PRIMARY]
                """;

            await using var connection = new SqlConnection(_sqlConnection);
            await connection.OpenAsync();
            await connection.ExecuteAsync(query);
        }

        public async Task<IEnumerable<Telegram>> GetDataAsync()
        {
            const string query = """
                SELECT [PROCESS] as Process
                    ,[SEQNUM] as SeqNum
                    ,[TELDT] as Teldt
                    ,[TELTYPE] as Teltype
                    ,[TELLEN] as Tellen
                    ,[IDSNDR] as Idsndr
                    ,[IDRCVR] as Idrcvr
                    ,[STATUS] as Status
                    ,[PAYLOAD] as Payload
                    ,[PKID] as Pkid
                FROM [dbo].[Telegrams]
                """;

            await using var connection = new SqlConnection(_sqlConnection);
            await connection.OpenAsync();

            return await connection.QueryAsync<Telegram>(query);
        }

        public async Task SaveDataAsync(Telegram data)
        {
            await using var connection = new SqlConnection(_sqlConnection);
            await connection.OpenAsync();

            string insertQuery = @"INSERT INTO [dbo].[Telegrams]
           ([PROCESS],[SEQNUM],[TELDT],[TELTYPE],[TELLEN],[IDSNDR],[IDRCVR],[STATUS],[PAYLOAD]) VALUES (@PROCESS,@SEQNUM,@TELDT,@TELTYPE,@TELLEN,@IDSNDR,@IDRCVR,@STATUS,@PAYLOAD)";

            var result = await connection.ExecuteAsync(insertQuery, new
            {
                data.Process,
                data.SeqNum,
                data.Teldt,
                data.Teltype,
                data.Tellen,
                data.Idsndr,
                data.Idrcvr,
                data.Status,
                data.Payload,
            });
        }
    }
}