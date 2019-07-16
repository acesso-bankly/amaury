namespace Amaury.Sample.MediatR.Domain.Entities
{
    public class Phone
    {
        public Phone(string ddi, string number)
        {
            DDI = ddi;
            Number = number;
        }

        public string DDI { get; }
        
        public string Number { get;  }

        public override string ToString() => $"{DDI}{Number}";
    }
}
