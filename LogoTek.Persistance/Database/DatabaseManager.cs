using Dapper;
using LogoTek.Application.Infrastructure.DatabaseManager;
using LogoTek.Domain.Models;
using Microsoft.Data.SqlClient;

namespace LogoTek.Persistance.Database
{
    public class DatabaseManager : IDatabaseManager
    {
        public DatabaseManager()
        { }

        public string SqlConnection { get; set; }

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

            using var connection = new SqlConnection(SqlConnection);
            await connection.OpenAsync().ConfigureAwait(false);
            await connection.ExecuteAsync(query).ConfigureAwait(false);
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

            using var connection = new SqlConnection(SqlConnection);
            await connection.OpenAsync().ConfigureAwait(false);

            return await connection.QueryAsync<Telegram>(query).ConfigureAwait(false);
        }

        public async Task SaveDataAsync(Telegram data)
        {
            using var connection = new SqlConnection(SqlConnection);
            await connection.OpenAsync().ConfigureAwait(false);

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
            }).ConfigureAwait(false);
        }
    }
}