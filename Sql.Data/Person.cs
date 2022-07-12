using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql.Data
{
    [Table(name:"T_Persons")]
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
