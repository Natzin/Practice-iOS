using System;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

namespace Practice2
{
    public class Ubication : TableEntity
    {
        public Ubication(String strFile_I, String strCountry_I)
        {
            this.PartitionKey = strFile_I;
            this.RowKey = strCountry_I;
        }
        public Ubication(){}
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String locality { get; set; }
    }
}
