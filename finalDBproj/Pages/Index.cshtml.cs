using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace finalDBproj.Pages;

    public class IndexModel : PageModel
    {

        public string placehold;

        public string WCTtableW;
        public string WCTtableL;
        public string Wtable;
        public string Ltable;
        public string WPtable;
        public string LPtable;

        public void OnGet()
        {            
            //Build the connection string
            //local connection string

            //Azure connectionstring
            string connectionString = "Server=tcp:mycoreappdbserver.database.windows.net,1433;Initial Catalog=dataMus;Persist Security Info=False;User ID=cs190admin;Password=class190besties!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            // Connect to a database
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // Define a query for each table
            SqlCommand WCTcommand = new SqlCommand("SELECT * FROM Teamset", conn);
            SqlDataReader WCTreader = WCTcommand.ExecuteReader();  
            DataTable WCTdt = new DataTable();
            WCTdt.Load(WCTreader);

            SqlCommand LCTcommand = new SqlCommand("SELECT * FROM Teamset", conn);
            SqlDataReader LCTreader = LCTcommand.ExecuteReader();  
            DataTable LCTdt = new DataTable();
            LCTdt.Load(LCTreader);

            SqlCommand Wcommand = new SqlCommand("SELECT * FROM Fullteam", conn);
            SqlDataReader Wreader = Wcommand.ExecuteReader();  
            DataTable Wdt = new DataTable();
            Wdt.Load(Wreader);

            SqlCommand Lcommand = new SqlCommand("SELECT * FROM Fullteam", conn);
            SqlDataReader Lreader = Lcommand.ExecuteReader();  
            DataTable Ldt = new DataTable();
            Ldt.Load(Lreader);

            SqlCommand WPcommand = new SqlCommand("SELECT * FROM Players", conn);
            SqlDataReader WPreader = WPcommand.ExecuteReader();  
            DataTable WPdt = new DataTable();
            WPdt.Load(WPreader);

            SqlCommand LPcommand = new SqlCommand("SELECT * FROM Players", conn);
            SqlDataReader LPreader = LPcommand.ExecuteReader();  
            DataTable LPdt = new DataTable();
            LPdt.Load(LPreader);

            List<string> PH = new List<string> {};

            List<string> WCTheaders = new List<string>
            {
            "team_id", "Team Name", "Team City", "Team State"
            };

            List<string> Teamheaders = new List<string>
            {
            "team_id", "Number", "Name", "Draft Year", "Contract Length"
            };

            List<string> Playerheaders = new List<string>
            {
            "team_id", "Number", "Points", "Assists", "Rebounds", "Minutes"
            };

            DTtoHTML(WCTdt, false, WCTheaders, false, "0001");
            WCTtableW = placehold;
            DTtoHTML(LCTdt, false, WCTheaders, false, "0002");
            WCTtableL = placehold;
            DTtoHTML(Wdt, true, Teamheaders, true, "0001");
            Wtable = placehold;
            DTtoHTML(Ldt, true, Teamheaders, true, "0002");
            Ltable = placehold;
            DTtoHTML(WPdt, true, Playerheaders, true, "0001");
            WPtable = placehold;
            DTtoHTML(LPdt, true, Playerheaders, true, "0002");
            LPtable = placehold;

            conn.Close();

            //translate to html, bounds for columns, header choice, public string assignment, and if I want to select one of two rows
            void DTtoHTML(DataTable data, bool headers, List<string> headlist, bool borders, string team_id)
            {
            string html = "<table style=\"margin: auto\">";
                //headers
                if (headers)
                {
                    html += ("<tr >");
                    for (int a = 1; a < data.Columns.Count; a++)
                        html += ("<td><i>| ") + headlist[a] + (" |<i></td>");
                    html += ("</tr>");
                }
                //rows
                for (int a = 0; a < data.Rows.Count; a++)
                {
                    if ((string)data.Rows[a][0] == team_id)
                    {
                        if (borders)
                        {
                            html += ("<tr style=\"margin: auto; border: 1px solid black\">");
                        }
                        for (int b = 1; b < data.Columns.Count; b++)
                            if (headers)
                            {
                                html += ("<td>") + data.Rows[a][b].ToString() + ("</td>");
                            }
                            else
                            {
                                html += ("<td><b>") + data.Rows[a][b].ToString() + (", </b></td>");
                            }
                        html += ("</tr>");
                    }
                }
                html += ("</table>");

                placehold = html;
            }
        }
    }
    
