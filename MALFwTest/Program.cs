using JikanDotNet;


public class AnimeInfo()
{
    private string title;
    private double? score;
    private string type;
    private string genres;
    private int? episodes;
    //private string seasons;
    private int? leaderboardRating;

    public async Task GetAnimeInfo(int animeId)
    {
        IJikan jikan = new Jikan();
        var anime = await jikan.GetAnimeAsync(animeId);
        var moreInfo = await jikan.GetAnimeMoreInfoAsync(animeId);
        title = anime.Data.Title;
        score = anime.Data.Score;
        type = anime.Data.Type;
        genres = string.Join(", ", anime.Data.Genres.Select(g => g.Name)); // Idk not a normal data type
        episodes = anime.Data.Episodes;
        //seasons = anime.Data.Related;
        leaderboardRating = anime.Data.Rank;
    }

    public void DisplayAllInfo()
    {
        string episodeNumber = episodes == null ? "More than 100" : episodes.ToString();
        string displayScore = score.HasValue ? score.Value.ToString("F2") : "N/A";
        string displayLeaderboardRating = leaderboardRating.HasValue ? leaderboardRating.ToString() : "N/A";
        string animeType = type == "TV" ? "Series" : type;

        Console.WriteLine($"Title: {title}, " +
            $"Score: {displayScore}, " +
            $"Type: {animeType}, " +
            $"Genres: {genres}, " +
            $"No. episodes: {episodeNumber}, " +
            //$"No. seasons/movies: {seasons}, " +
            $"Leaderboard number: {displayLeaderboardRating}");
    }
}


public class Program
{
    public static async Task Main(string[] args)
    {
        AnimeInfo animeInfo = new AnimeInfo();
        var animeList = new List<int> { 21, 16498, 52991 };
        foreach (var i in animeList)
        {
            int animeId = i;
            await animeInfo.GetAnimeInfo(animeId);
            animeInfo.DisplayAllInfo();
            Thread.Sleep(200);
        }
    }
}
