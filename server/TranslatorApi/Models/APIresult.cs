using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApi.Models
{
    public class APIresult<T>
    {
        public bool state;
        public string errorMessage;
        public T data;

        public APIresult(bool state, string message, T data)
        {
            this.state = state;
            this.errorMessage = message;
            this.data = data;
        }
    }
}