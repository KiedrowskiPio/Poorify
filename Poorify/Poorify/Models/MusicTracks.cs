using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Poorify.Models
{
    public class MusicTracks
    {
        [Required]
        public int TrackId { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = true)]
        public string TrackName { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = true)]
        public string TrackAlbum { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = true)]
        public string TrackArtist { get; set; }

        [Required]
        public int TrackLength { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime TrackDate { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string PhotoFileName { get; set; }
    }
}
