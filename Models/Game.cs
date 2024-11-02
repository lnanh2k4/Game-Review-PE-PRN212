using System;
using System.Collections.Generic;

namespace Games.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string? GameName { get; set; }

    public string? Genre { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? Price { get; set; }
}
