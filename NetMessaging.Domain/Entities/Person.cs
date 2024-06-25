namespace NetMessaging.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AuthorName { get; set; }
        public DateTime ProcessDate { get; set; }
        public InsurancePayment InsurancePayment { get; set; }
    }
}
