using Games.Models;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Games.Utilities;
using System.Collections.ObjectModel;
using System.Windows;

namespace Games.ViewModels
{
    public class GameVM : BaseVM
    {
        public ObservableCollection<Game> Games { get; set; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand UpdateCommand { get; }

        public GameVM()
        {
            NewItem = new Game();
            Games = new ObservableCollection<Game>();
            LoadData();
            AddCommand = new RelayCommand(ADD);
            UpdateCommand = new RelayCommand(UPDATE);
            RemoveCommand = new RelayCommand(REMOVE);
        }

        private void REMOVE(object obj)
        {
            using (var context = new GamesContext())
            {
                if (SelectedItem != null)
                {
                    context.Games.Remove(SelectedItem);
                    context.SaveChanges();
                    LoadData();
                    NewItem = new Game();
                }
            }
        }

        private void UPDATE(object obj)
        {
            using (var context = new GamesContext())
            {
                if (SelectedItem != null)
                {
                    var item = context.Games.Where(x => x.GameId == NewItem.GameId).FirstOrDefault();
                    if (item != null)
                    {
                        item.GameId = NewItem.GameId;
                        item.Price = NewItem.Price;
                        item.GameName = NewItem.GameName;
                        item.Genre = NewItem.Genre;
                        item.ReleaseDate = NewItem.ReleaseDate;
                        context.SaveChanges();
                        LoadData();
                        NewItem = new Game();
                    }

                }
            }
        }

        private void ADD(object obj)
        {
            using (var context = new GamesContext())
            {
                if (NewItem != null)
                {
                    NewItem.GameId = 0;
                    context.Games.Add(NewItem);
                    context.SaveChanges();
                    Games.Add(NewItem);
                    NewItem = new Game();
                }

            }
        }

        private void LoadData()
        {
            using (var context = new GamesContext())
            {
                var items = context.Games.ToList();
                Games.Clear();
                foreach (var item in items)
                {
                    Games.Add(item);
                }
            }
        }

        private Game _selecteditem;
        public Game SelectedItem
        {
            get { return _selecteditem; }
            set
            {
                _selecteditem = value;
                OnPropertyChanged(nameof(SelectedItem));
                if (SelectedItem != null)
                {
                    NewItem = new Game
                    {
                        GameId = SelectedItem.GameId,
                        GameName = SelectedItem.GameName,
                        Genre = SelectedItem.Genre,
                        Price = SelectedItem.Price,
                        ReleaseDate = SelectedItem.ReleaseDate,
                    };
                    OnPropertyChanged(nameof(NewItem));
                }
            }
        }

        private Game _newitem;
        public Game NewItem
        {
            get { return _newitem; }
            set
            {
                _newitem = value;
                OnPropertyChanged(nameof(NewItem));
            }
        }
    }
}
