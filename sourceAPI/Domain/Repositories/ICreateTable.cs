using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServ.Domain.Repositories
{
    public interface ICreateTable
    {
        void CreateDynamoDbTable();
    }
}
