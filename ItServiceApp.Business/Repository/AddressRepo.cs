using ItServiceApp.Business.Repository.Abstracts;
using ItServiceApp.Core.Entities;
using ItServiceApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItServiceApp.Business.Repository
{
    public class AddressRepo:BaseRepository<Address, Guid>
    {
        public AddressRepo(MyContext context):base(context)
        {

        }
    }
}
