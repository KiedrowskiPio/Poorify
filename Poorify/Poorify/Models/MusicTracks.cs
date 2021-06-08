using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poorify.Models
{
    public class MusicTracks
    {

        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public string TrackAlbum { get; set; }
        public string TrackArtist { get; set; }
        public int TrackLength { get; set; }
        public DateTime TrackDate { get; set; }
        public string PhotoFileName { get; set; }

    }
}
