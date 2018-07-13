using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for EpCapsList
    /// </summary>
    public static class EpCapsList
    {
        public static List<EpisodeTitle> S1List = new List<EpisodeTitle>()
        {
            // Season 1
            new EpisodeTitle() { Title = "Crush", UrlLabel = "crush", CDNPath = "Crush", Count = 1258 },
            new EpisodeTitle() { Title = "Sink Or Swim", UrlLabel = "sink-or-swim", CDNPath = "Sink Or Swim", Count = 1265 },
            new EpisodeTitle() { Title = "The New Ron", UrlLabel = "the-new-ron", CDNPath = "The New Ron", Count = 1258 },
            new EpisodeTitle() { Title = "Tick Tick Tick", UrlLabel = "tick-tick-tick", CDNPath = "Tick Tick Tick", Count = 1261 },
            new EpisodeTitle() { Title = "Downhill", UrlLabel = "downhill", CDNPath = "Downhill", Count = 1265 },
            new EpisodeTitle() { Title = "Bueno Nacho", UrlLabel = "bueno-nacho", CDNPath = "Bueno Nacho", Count = 1264 },
            new EpisodeTitle() { Title = "Number One", UrlLabel = "number-one", CDNPath = "Number One", Count = 1263 },
            new EpisodeTitle() { Title = "Mind Games", UrlLabel = "mind-games", CDNPath = "Mind Games", Count = 1267 },
            new EpisodeTitle() { Title = "Attack of the Killer Bebes", UrlLabel = "attack-of-the-killer-bebes", CDNPath = "Attack of the Killer Bebes", Count = 1259 },
            new EpisodeTitle() { Title = "Royal Pain", UrlLabel = "royal-pain", CDNPath = "Royal Pain", Count = 1264 },
            new EpisodeTitle() { Title = "Coach Possible", UrlLabel = "coach-possible", CDNPath = "Coach possible", Count = 1264 },
            new EpisodeTitle() { Title = "Pain King vs. Cleopatra", UrlLabel = "pain-king-vs-cleopatra", CDNPath = "Pain King vs Cleopatra", Count = 1265 },
            new EpisodeTitle() { Title = "Monkey Fist Strikes", UrlLabel = "monkey-fist-strikes", CDNPath = "Monkey Fist Strikes", Count = 1261 },
            new EpisodeTitle() { Title = "October 31st", UrlLabel = "october-31st", CDNPath = "October 31st", Count = 1260 },
            new EpisodeTitle() { Title = "All the News", UrlLabel = "all-the-news", CDNPath = "All the News", Count = 1263 },
            new EpisodeTitle() { Title = "Kimitation Nation", UrlLabel = "kimitation-nation", CDNPath = "Kimitation Nation", Count = 1260 },
            new EpisodeTitle() { Title = "The Twin Factor", UrlLabel = "the-twin-factor", CDNPath = "The Twin Factor", Count = 1261 },
            new EpisodeTitle() { Title = "Animal Attraction", UrlLabel = "animal-attraction", CDNPath = "Animal Attraction", Count = 1259 },
            new EpisodeTitle() { Title = "Monkey Ninjas in Space", UrlLabel = "monkey-ninjas-in-space", CDNPath = "Monkey Ninjas In Space", Count = 1261 },
            new EpisodeTitle() { Title = "Ron the Man", UrlLabel = "ron-the-man", CDNPath = "Ron the Man", Count = 1258 },
            new EpisodeTitle() { Title = "Low Budget", UrlLabel = "low-budget", CDNPath = "Low Budget", Count = 1259 }
        };
        public static List<EpisodeTitle> S2List = new List<EpisodeTitle>()
        {
            // Season 2
            new EpisodeTitle() { Title = "Naked Genius", UrlLabel = "naked-genius", CDNPath = "Naked Genius", Count = 1255 },
            new EpisodeTitle() { Title = "Grudge Match", UrlLabel = "grudge-match", CDNPath = "Grudge Match", Count = 1262 },
            new EpisodeTitle() { Title = "Two to Tutor", UrlLabel = "two-to-tutor", CDNPath = "Two to Tutor", Count = 1259 },
            new EpisodeTitle() { Title = "The Ron Factor", UrlLabel = "the-ron-factor", CDNPath = "The Ron Factor", Count = 1266 },
            new EpisodeTitle() { Title = "Car Trouble", UrlLabel = "car-trouble", CDNPath = "Car Trouble", Count = 1260 },
            new EpisodeTitle() { Title = "Rufus in Show", UrlLabel = "rufus-in-show", CDNPath = "Rufus in Show", Count = 627 },
            new EpisodeTitle() { Title = "Adventures in Rufus-Sitting", UrlLabel = "adventures-in-rufus-sitting", CDNPath = "Adventures in Rufus-Sitting", Count = 627 },
            new EpisodeTitle() { Title = "Job Unfair", UrlLabel = "job-unfair", CDNPath = "Job Unfair", Count = 1265 },
            new EpisodeTitle() { Title = "The Golden Years", UrlLabel = "the-golden-years", CDNPath = "The Golden Years", Count = 1267 },
            new EpisodeTitle() { Title = "Vir-Tu-Ron", UrlLabel = "vir-tu-ron", CDNPath = "Virtu-Ron", Count = 1264 },
            new EpisodeTitle() { Title = "The Fearless Ferret", UrlLabel = "the-fearless-ferret", CDNPath = "The Fearless Ferret", Count = 1263 },
            new EpisodeTitle() { Title = "Exchange", UrlLabel = "exchange", CDNPath = "Exchange", Count = 1265 },
            new EpisodeTitle() { Title = "Rufus vs. Commodore Puddles", UrlLabel = "rufus-vs-commodore-puddles", CDNPath = "Rufus vs Commodore Puddles", Count = 630 },
            new EpisodeTitle() { Title = "Day of the Snowmen", UrlLabel = "day-of-the-snowmen", CDNPath = "Day of the Snowmen", Count = 630 },
            new EpisodeTitle() { Title = "A Sitch in Time: Present (1)", UrlLabel = "a-sitch-in-time-present-1", CDNPath = "ASiT 1 Present", Count = 1269 },
            new EpisodeTitle() { Title = "A Sitch in Time: Past (2)", UrlLabel = "a-sitch-in-time-past-2", CDNPath = "ASiT 2 Past", Count = 1268 },
            new EpisodeTitle() { Title = "A Sitch in Time: Future (3)", UrlLabel = "a-sitch-in-time-future-3", CDNPath = "ASiT 3 Future", Count = 1271 },
            new EpisodeTitle() { Title = "A Very Possible Christmas", UrlLabel = "a-very-possible-christmas", CDNPath = "A Very Possible Christmas", Count = 1265 },
            new EpisodeTitle() { Title = "Queen Bebe", UrlLabel = "queen-bebe", CDNPath = "Queen Bebe", Count = 1267 },
            new EpisodeTitle() { Title = "Hidden Talent", UrlLabel = "hidden-talent", CDNPath = "Hidden Talent", Count = 1267 },
            new EpisodeTitle() { Title = "Return to Camp Wannaweep", UrlLabel = "return-to-camp-wannaweep", CDNPath = "Return to Camp Wannaweep", Count = 1270 },
            new EpisodeTitle() { Title = "Go Team Go", UrlLabel = "go-team-go", CDNPath = "Go Team Go", Count = 1267 },
            new EpisodeTitle() { Title = "The Full Monkey", UrlLabel = "the-full-monkey", CDNPath = "The Full Monkey", Count = 1268 },
            new EpisodeTitle() { Title = "Blush", UrlLabel = "blush", CDNPath = "Blush", Count = 1269 },
            new EpisodeTitle() { Title = "Partners", UrlLabel = "partners", CDNPath = "Partners", Count = 1258 },
            new EpisodeTitle() { Title = "Oh Boyz", UrlLabel = "oh-boyz", CDNPath = "Oh Boyz", Count = 1267 },
            new EpisodeTitle() { Title = "Sick Day", UrlLabel = "sick-day", CDNPath = "Sick Day", Count = 630 },
            new EpisodeTitle() { Title = "The Truth Hurts", UrlLabel = "the-truth-hurts", CDNPath = "Truth Hurts", Count = 630 },
            new EpisodeTitle() { Title = "Mother's Day", UrlLabel = "mothers-day", CDNPath = "Mothers day", Count = 1269 },
            new EpisodeTitle() { Title = "Motor Ed", UrlLabel = "motor-ed", CDNPath = "Motor Ed", Count = 1262 },
            new EpisodeTitle() { Title = "Ron Millionaire", UrlLabel = "ron-millionaire", CDNPath = "Ron Millionaire", Count = 1266 },
            new EpisodeTitle() { Title = "Triple S", UrlLabel = "triple-s", CDNPath = "Triple S", Count = 1269 },
            new EpisodeTitle() { Title = "Rewriting History", UrlLabel = "rewriting-history", CDNPath = "Rewriting History", Count = 1258 },
            new EpisodeTitle() { Title = "Showdown at the Crooked D", UrlLabel = "showdown-at-the-crooked-d", CDNPath = "Showdown at the Crooked D", Count = 1266 }
        };
        public static List<EpisodeTitle> S3List = new List<EpisodeTitle>()
        {
            // Season 3
            new EpisodeTitle() { Title = "Steal Wheels", UrlLabel = "steal-wheels", CDNPath = "Steal Wheels", Count = 1258 },
            new EpisodeTitle() { Title = "Emotion Sickness", UrlLabel = "emotion-sickness", CDNPath = "Emotion Sickness", Count = 1269 },
            new EpisodeTitle() { Title = "Bonding", UrlLabel = "bonding", CDNPath = "Bonding", Count = 1268 },
            new EpisodeTitle() { Title = "Bad Boy", UrlLabel = "bad-boy", CDNPath = "Bad Boy", Count = 1261 },
            new EpisodeTitle() { Title = "Dimension Twist", UrlLabel = "dimension-twist", CDNPath = "Dimension Twist", Count = 1255 },
            new EpisodeTitle() { Title = "Overdue", UrlLabel = "overdue", CDNPath = "Overdue", Count = 629 },
            new EpisodeTitle() { Title = "Roachie", UrlLabel = "roachie", CDNPath = "Roachie", Count = 631 },
            new EpisodeTitle() { Title = "Rappin' Drakken", UrlLabel = "rappin-drakken", CDNPath = "Rappin Drakken", Count = 1265 },
            new EpisodeTitle() { Title = "Team Impossible", UrlLabel = "team-impossible", CDNPath = "Team Impossible", Count = 1266 },
            new EpisodeTitle() { Title = "Gorilla Fist", UrlLabel = "gorilla-fist", CDNPath = "Gorilla Fist", Count = 1272 },
            new EpisodeTitle() { Title = "And The Molerat Will Be CGI...", UrlLabel = "and-the-molerat-will-be-cgi", CDNPath = "And the Molerat Will Be CGI", Count = 1264 }
        };
        public static List<EpisodeTitle> S4List = new List<EpisodeTitle>()
        {
            // Season 4
            new EpisodeTitle() { Title = "Ill-Suited", UrlLabel = "ill-suited", CDNPath = "Ill-Suited", Count = 1284 },
            new EpisodeTitle() { Title = "Car Alarm", UrlLabel = "car-alarm", CDNPath = "Car Alarm", Count = 1278 },
            new EpisodeTitle() { Title = "Trading Faces", UrlLabel = "trading-faces", CDNPath = "Trading faces", Count = 1282 },
            new EpisodeTitle() { Title = "The Big Job", UrlLabel = "the-big-job", CDNPath = "The Big Job", Count = 1282 },
            new EpisodeTitle() { Title = "Mad Dogs and Aliens", UrlLabel = "mad-dogs-and-aliens", CDNPath = "Mad Dogs and Aliens", Count = 1282 },
            new EpisodeTitle() { Title = "Nursery Crimes", UrlLabel = "nursery-crimes", CDNPath = "Nusery Crimes", Count = 661 },
            new EpisodeTitle() { Title = "The Cupid Effect", UrlLabel = "the-cupid-effect", CDNPath = "The Cupid Effect", Count = 1282 },
            new EpisodeTitle() { Title = "Clothes Minded", UrlLabel = "clothes-minded", CDNPath = "Clothes Minded", Count = 1288 },
            new EpisodeTitle() { Title = "Fashion Victim", UrlLabel = "fashion-victim", CDNPath = "Fashion Victim", Count = 1280 },
            new EpisodeTitle() { Title = "Odds Man In", UrlLabel = "odds-man-in", CDNPath = "Odds Man In", Count = 1276 },
            new EpisodeTitle() { Title = "Grande Size Me", UrlLabel = "grande-size-me", CDNPath = "Grande Size Me", Count = 1286 },
            new EpisodeTitle() { Title = "Big Bother", UrlLabel = "big-bother", CDNPath = "Big Bother", Count = 1283 },
            new EpisodeTitle() { Title = "Clean Slate", UrlLabel = "clean-slate", CDNPath = "Clean Slate", Count = 1274 },
            new EpisodeTitle() { Title = "Mathter and Fervent", UrlLabel = "mathter-and-fervent", CDNPath = "Mathter and Fervent", Count = 1290 },
            new EpisodeTitle() { Title = "Homecoming Upset", UrlLabel = "homecoming-upset", CDNPath = "Homecoming Upset", Count = 1279 },
            new EpisodeTitle() { Title = "The Mentor of Our Discontent", UrlLabel = "mentor-of-our-discontent", CDNPath = "The Mentor of Our Discontent", Count = 1290 },
            new EpisodeTitle() { Title = "Cap'n Drakken", UrlLabel = "capn-drakken", CDNPath = "Capn Drakken", Count = 1286 },
            new EpisodeTitle() { Title = "Stop Team Go", UrlLabel = "stop-team-go", CDNPath = "Stop Team Go", Count = 1279 },
            new EpisodeTitle() { Title = "Larry's Birthday", UrlLabel = "larrys-birthday", CDNPath = "Larrys Birthday", Count = 1275 },
            new EpisodeTitle() { Title = "Oh No! Yono!", UrlLabel = "oh-no-yono", CDNPath = "Oh No Yono", Count = 1278 },
            new EpisodeTitle() { Title = "Chasing Rufus", UrlLabel = "chasing-rufus", CDNPath = "Chasing Rufus", Count = 619 },
            new EpisodeTitle() { Title = "Graduation Part 1", UrlLabel = "graduation-pt-1", CDNPath = "Graduation Pt 1", Count = 1272 },
            new EpisodeTitle() { Title = "Graduation Part 2", UrlLabel = "graduation-pt-2", CDNPath = "Graduation Pt 2", Count = 1276 }
        };
        public static List<EpisodeTitle> MovieList = new List<EpisodeTitle>()
        {
            // Movies and stuff
            new EpisodeTitle() { Title = "A Sitch in Time", UrlLabel = "a-sitch-in-time", CDNPath = "A Sitch in Time", Count = 3795 },
            new EpisodeTitle() { Title = "So the Drama", UrlLabel = "so-the-drama", CDNPath = "std", Count = 2700 }
        };
        public static List<EpisodeTitle> MiscList = new List<EpisodeTitle>()
        {
            // Miscellaneous
            new EpisodeTitle() { Title = "Lilo and Stitch: Rufus", UrlLabel = "ls-rufus", CDNPath = "LS Rufus", Count = 1256 },
            new EpisodeTitle() { Title = "Opening", UrlLabel = "opening", CDNPath = "Opening", Count = 266 },
            new EpisodeTitle() { Title = "Season 4 Opening", UrlLabel = "season-4-opening", CDNPath = "S4 Opening", Count = 284 },
            new EpisodeTitle() { Title = "Half Opening", UrlLabel = "half-opening", CDNPath = "Half Opening", Count = 45 },
            new EpisodeTitle() { Title = "The Secret Files", UrlLabel = "the-secret-files", CDNPath = "The Secret Files", Count = 1062 },
            new EpisodeTitle() { Title = "The Secret Files: Wacko Bad Guys", UrlLabel = "the-secret-files-wacko-bad-guys", CDNPath = "misc/TSF-WackoBadGuys", Count = 80 },
            new EpisodeTitle() { Title = "Could it Be", UrlLabel = "could-it-be", CDNPath = "misc/cib", Count = 247 },
            new EpisodeTitle() { Title = "Get Your Shine On", UrlLabel = "get-your-shine-on", CDNPath = "misc/gyso", Count = 188 },
            new EpisodeTitle() { Title = "It's Just You", UrlLabel = "its-just-you", CDNPath = "misc/ItsJustYou", Count = 349 },
            new EpisodeTitle() { Title = "Say the Word", UrlLabel = "say-the-word", CDNPath = "misc/STW", Count = 329 },
            new EpisodeTitle() { Title = "The Naked Mole Rap", UrlLabel = "the-naked-mole-rap", CDNPath = "misc/nmr", Count = 237 },
            new EpisodeTitle() { Title = "So the Drama Deleted Scenes", UrlLabel = "so-the-drama-deleted-scenes", CDNPath = "misc/StD-DeletedScenes", Count = 120 },
            new EpisodeTitle() { Title = "Kim Possible Ad (from The Secret Files)", UrlLabel = "kim-possible-tv-ad", CDNPath = "misc/KP-Ad-TSF", Count = 60 },
            new EpisodeTitle() { Title = "The Villain Files Opening", UrlLabel = "the-villain-files-opening", CDNPath = "misc/TVF_Opening", Count = 181 },
            new EpisodeTitle() { Title = "The Villain Files: Villain Party", UrlLabel = "the-villain-files-villain-party", CDNPath = "misc/TVF-VillainParty", Count = 115 },
            new EpisodeTitle() { Title = "Tick Tick Tick Additional", UrlLabel = "tick-tick-tick-additional", CDNPath = "misc/tick-tick-tick", Count = 256 },
            new EpisodeTitle() { Title = "Rappin' Drakken Additional", UrlLabel = "rappin-drakken-additional", CDNPath = "misc/rappin-drakken", Count = 1 },
            new EpisodeTitle() { Title = "Other (Memes, etc)", UrlLabel = "other-memes-etc", CDNPath = "other", Count = 39 }
        };
    }
}