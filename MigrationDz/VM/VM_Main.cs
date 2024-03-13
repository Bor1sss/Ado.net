using Author.Commands;
using DBcontextLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
using MigrationDz.VM;
using ModelStruct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Style = ModelStruct.Style;

namespace MigrationDz.VM
{
    public class VM_Main : VM_Base
    {
        public ObservableCollection<VM_Game> GameList { get; set; }
        public ObservableCollection<VM_Studio> StudioList { get; set; }
        public ObservableCollection<VM_Style> StyleList { get; set; }
     

        public VM_Main(IQueryable<Game> games, IQueryable<Studio> studios, IQueryable<Style> styles)
        {
            GameList = new ObservableCollection<VM_Game>(games.Select(g => new VM_Game(g)));
            StudioList = new ObservableCollection<VM_Studio>(studios.Select(s => new VM_Studio(s)));
            StyleList = new ObservableCollection<VM_Style>(styles.Select(s => new VM_Style(s)));

        }

        int SelectedIndex = -1;
        public int selectedIndex
        {
            get { return SelectedIndex; }
            set
            {
                SelectedIndex = value;
                OnPropertyChanged(nameof(selectedIndex));
                if(SelectedIndex != -1)
                {
                    ChangeData();
                }
            }
        }

        public void ChangeData()
        {
            ChangeStudio();
            ChangeStyle();
        }
         public void ChangeStudio()
        {

            using (var db = new GameContext())
            {
                var Game = GameList[selectedIndex];
                var studioQuery = from studio in db.Studios
                                  where studio.Name == Game.StudioName
                                  select studio;
                StudioList = new ObservableCollection<VM_Studio>(studioQuery.Select(g => new VM_Studio(g)));
                OnPropertyChanged(nameof(StudioList));
            }
         }

        public void ChangeStyle()
        {
            using (var db = new GameContext())
            {
                var Game = GameList[selectedIndex];
                var game = (from g in db.Games
                                  where g.Name == Game.Name
                                  select g).Single();


                List<Style> list = db.Styles.Include(l => l.Games).ToList();
             

                StyleList = new ObservableCollection<VM_Style>(game.Styles.Select(g => new VM_Style(g)));
                OnPropertyChanged(nameof(StyleList));

            }
        }














    }

}
