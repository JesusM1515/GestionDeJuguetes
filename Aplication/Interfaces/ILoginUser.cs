using Application.DTO;
using Domain.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILoginUser
    {
        Task<OperationResult<LoginDTO>> Login(LoginDTO login);
    }
}
