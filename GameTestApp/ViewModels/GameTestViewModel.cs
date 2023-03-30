using GameTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTestApp.ViewModels
{
    public class GameTestViewModel : BaseViewModel
    {
        public List<Option> Options { get; set; }
        public string pregunta { get; set; } = "";
    }
}
