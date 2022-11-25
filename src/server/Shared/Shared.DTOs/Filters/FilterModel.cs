using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Filters
{
    public class FilterModel
    {
        private const string DefaultOpr = "=";
        private string _action = DefaultOpr;

        public string FieldName { get; set; }

        public string SearchTerm { get; set; }

        public string Action
        {
            get => _action;
            set => _action = string.IsNullOrEmpty(value) ? DefaultOpr : value;
        }
    }
}