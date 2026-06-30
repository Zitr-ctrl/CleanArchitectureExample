using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BrandEntity
    {
        private string _name;
        // El signo de interrogación al final del tipo de dato hace referencia de que el eleemnto
        // no puede ser null
        public int? Id {  get; private set; }

        public string Name
        {
            get => _name;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre no puede estar vacío", nameof(value));
                }
                _name = value;
            }
        }

        public BrandEntity()
        {

        }
        public BrandEntity(string name)
        {
            Name = name;
        }

        public BrandEntity(int id, string name)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id),"Id debe ser positivo" );
            }

            Id = id;
            Name = name;
        }
    }
}
