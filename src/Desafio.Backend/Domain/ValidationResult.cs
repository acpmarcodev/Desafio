using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Domain
{
    public enum ValidationResult
    {
        Ok = 1,
        InvalidBirthday,
        InvalidDocument,
        InvalidEmail,
        MissingEmail,
        MissingLastName,
        MissingName,
        InvalidOperator,
        InvalidRoleId,
    }
}
