using Microsoft.EntityFrameworkCore;

namespace RMSServices.Models
{
    public class RequestWrapper
    {
        public List<Filter> Filter { get; set; } = new List<Filter>();
        public string FilterString { get; set; }
        public Sort Sort { get; set; } = new Sort();

        public Limit Limit { get; set; }= new Limit();


    }
    public class Sort
    {
        public string Property { get; set; } = "ID";
        public string Direction { get; set; } = "ASC";
    }
    public class Limit
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;

    }
    public class Filter
    {
        public string Property { get; set; }
        public string Comparator { get; set; }
        public string Value { get; set; }

    }


}
