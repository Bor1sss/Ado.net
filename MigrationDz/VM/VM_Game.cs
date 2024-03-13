
using ModelStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationDz.VM
{
    public class VM_Game : VM_Base
    {
        private Game _game;

        public VM_Game(Game game)
        {
            _game = game;
        }

        public DateTime Realise
        {
            get { return _game.Realise; }
            set
            {
                _game.Realise = value;
                OnPropertyChanged(nameof(Realise));
            }
        }
        public string Name
        {
            get { return _game.Name; }
            set
            {
                _game.Name = value;
                OnPropertyChanged(nameof(Realise));
            }
        }
        public string StudioName
        {
            get { return _game.Studio.Name; }
        }

        public int? Copies
        {
            get { return _game.Copies; }
        }
        public string? GameType
        {
            get { return _game.GameType; }
        }
    }
}
