namespace GreaTreasure.DAL
{
    public class Data
    {
        string ConnectionString = "server=ELAD\\SA ;initial catalog=GreaTreasure; user id=sa; password=1234; TrustServerCertificate=Yes;";
        static Data _data;
        private  DataLayer DataLayer;
        private Data()
        {
            DataLayer  = new DataLayer(ConnectionString);
        }
        public static DataLayer Get
        {
            get {
                if (_data == null)
                    _data = new Data();
                return _data.DataLayer;
            }
        }
    }
}