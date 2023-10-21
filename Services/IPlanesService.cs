using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDTelecomunicaciones.Models;

namespace JDTelecomunicaciones.Services
{
    public interface IPlanesService
    {
        public List<Planes> GetAllPlans();
    }
}