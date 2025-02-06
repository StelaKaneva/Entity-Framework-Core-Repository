namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //var result = ExportAlbumsInfo(context, 9);
            //Console.WriteLine(result);

            var result = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context
                        .Producers
                        .FirstOrDefault(x => x.Id == producerId)
                        .Albums
                        .Select(album => new
                        {
                            AlbumName = album.Name,
                            ReleaseDate = album.ReleaseDate,
                            ProducerName = album.Producer.Name,
                            Songs = album.Songs.Select(song => new
                            {
                                SongName = song.Name,
                                Price = song.Price,
                                Writer = song.Writer.Name,
                            })
                            .OrderByDescending(song => song.SongName)
                            .ThenBy(song => song.Writer)
                            .ToList(),
                            AlbumPrice = album.Price
                        })
                        .OrderByDescending(album => album.AlbumPrice)
                        .ToList();

            var sb = new StringBuilder();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine("-Songs:");

                int counter = 1;

                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{counter++}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.Price:f2}");
                    sb.AppendLine($"---Writer: {song.Writer}");
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }

            return sb.ToString().TrimEnd();          
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var allSongs = context.Songs
                           //.ToList()
                           //.Where(song => song.Duration.Seconds > duration)
                           .Where(x => x.Duration > TimeSpan.FromSeconds(duration))
                           .Select(song => new
                           {
                               SongName = song.Name,
                               Writer = song.Writer.Name,
                               PerformerFullName = song.SongPerformers
                                                   .Select(song => song.Performer.FirstName + " " + song.Performer.LastName).FirstOrDefault(),
                               AlbumProducer = song.Album.Producer.Name,
                               Duration = song.Duration
                           })
                           .OrderBy(song => song.SongName)
                           .ThenBy(song => song.Writer)
                           .ThenBy(song => song.PerformerFullName);

            var sb = new StringBuilder();

            int counter = 1;

            foreach (var song in allSongs)
            {
                sb.AppendLine($"-Song #{counter++}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.Writer}");
                sb.AppendLine($"---Performer: {song.PerformerFullName}");
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration:c}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
