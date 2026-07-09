
using Godot;
using Overlay.Core.Services.JoystickBots;
using Overlay.Core.Services.Joysticks.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Text.RegularExpressions.Regex;

namespace Overlay.Core.Contents;

public sealed partial class VoiceController() :
	Node()
{
	public override void _ExitTree()
	{
		base._ExitTree();

		this.StopProcessingPeer();
	}
	
	public override void _Ready()
	{
		base._Ready();
		
		this.BindPeer();
		this.StartProcessingPeer();
	}

	private const int                                  c_port                = 4242;
	private const int                                  c_delayInMilliseconds = 16;
	private const string                               c_username            = $"SmoothDagger";

	private static readonly Dictionary<string, string> s_openTokens          = new()
	{
		{ "ask",  "ask" },
		{ "ax",   "ask" },
		{ "axe",  "ask" },
		{ "ast",  "ask" },
		{ "askt", "ask" },
		{ "asp",  "ask" },
		{ "act",  "ask" },
		{ "esk",  "ask" },
		{ "ex",   "ask" },
		
		// request
		{ "request",    "request" },
		{ "re quest",   "request" },
		{ "bequest",    "request" },
		{ "regress",    "request" },
		{ "re-quest",   "request" },
		{ "we quest",   "request" },
		{ "requesting", "request" },
	};
	private static readonly Dictionary<string, string> s_nameMap             = new()
	{
		{ "a dagger", "dagger" },
		{ "agger",    "dagger" },
		{ "bagger",   "dagger" },
		{ "beggar",   "dagger" },
		{ "bigger",   "dagger" },
		{ "dad ger",  "dagger" },
		{ "dag",      "dagger" },
		{ "dag her",  "dagger" },
		{ "dagger",   "dagger" },
		{ "dagg er",  "dagger" },
		{ "daggered", "dagger" },
		{ "daggers",  "dagger" },
		{ "deck her", "dagger" },
		{ "digger",   "dagger" },
		{ "stagger",  "dagger" },
		{ "tagger",   "dagger" },
		{ "tiger",    "dagger" },
	};
	
	private static readonly Dictionary<string, string> s_numberMap           = new()
	{
		{
			$"zero",
			$"0"
		},
		{
			$"one",
			$"1"
		},
		{
			$"two",
			$"2"
		},
		{
			$"three",
			$"3"
		}
	};
	private static readonly Dictionary<string, string> s_vocabularyPhrases   = new()
	{ 
		#region Commands
		
		// ask
		{ "ask",  "ask" },
		{ "ax",   "ask" },
		{ "axe",  "ask" },
		{ "ast",  "ask" },
		{ "askt", "ask" },
		{ "asp",  "ask" },
		{ "act",  "ask" },
		{ "esk",  "ask" },
		{ "ex",   "ask" },
		
		// avatar
	    { "avatar",  "avatar" },
	    { "abatar",  "avatar" },
	    { "appatar", "avatar" },
	    { "abbatar", "avatar" },
	    { "amatar",  "avatar" },
	    { "avtar",   "avatar" },
	    { "abtar",   "avatar" },
	    { "aptar",   "avatar" },
	    { "ovatar",  "avatar" },
	    { "uvatar",  "avatar" },
	    { "avata",   "avatar" },
	    { "after",   "avatar" },
	    { "habatar", "avatar" },

	    // avatars
	    { "avatars",   "avatars" },
	    { "avatarz",   "avatars" },
	    { "avatar is", "avatars" },
	    { "avatar as", "avatars" },
	    { "avatar us", "avatars" },
	    { "avtars",    "avatars" },
	    { "abtars",    "avatars" },
	    { "aptars",    "avatars" },
	    { "avatarsh",  "avatars" },
	    { "avatarish", "avatars" },

	    // away from keyboard
	    { "away from keyboard",     "afk" },
	    { "afk",                    "afk" },
	    { "a f k",                  "afk" },
	    { "away keyboard",          "afk" },
	    { "away from the keyboard", "afk" },
	    { "way from keyboard",      "afk" },
	    { "way keyboard",           "afk" },
	    { "a way from keyboard",    "afk" },
	    { "away from key board",    "afk" },
	    { "away front keyboard",    "afk" },
	    { "away from key bored",    "afk" },
	    { "a way for keyboard",     "afk" },
	    { "always from keyboard",   "afk" },
	    { "wait from keyboard",     "afk" },

	    // badge
	    { "badge",   "badge" },
	    { "bash",    "badge" },
	    { "batch",   "badge" },
	    { "bad",     "badge" },
	    { "badged",  "badge" },
	    { "bach",    "badge" },
	    { "bags",    "badge" },
	    { "bashed",  "badge" },
	    { "batched", "badge" },
	    { "page",    "badge" },
	    { "pash",    "badge" },

	    // base
	    { "base",  "base" },
	    { "bace",  "base" },
	    { "bass",  "base" },
	    { "pace",  "base" },
	    { "vase",  "base" },
	    { "phase", "base" },
	    { "baise", "base" },
	    { "based", "base" },
	    { "baste", "base" },
	    { "beast", "base" },
	    { "face",  "base" },

	    // break
	    { "break",   "break" },
	    { "brake",   "break" },
	    { "brick",   "break" },
	    { "breaked", "break" },
	    { "rake",    "break" },
	    { "freak",   "break" },
	    
	    // check
	    { "check",   "check" },
	    { "chick",   "check" },
	    { "cheque",  "check" },
	    { "shack",   "check" },
	    { "jack",    "check" },
	    { "checkt",  "check" },
	    { "checked", "check" },

	    // code
	    { "code",  "code" },
	    { "could", "code" },
	    { "coat",  "code" },
	    { "cold",  "code" },
	    { "coded", "code" },
	    { "coed",  "code" },
	    { "ode",   "code" },

	    // color
	    { "color",  "color" },
	    { "collar", "color" },
	    { "culler", "color" },
	    { "cover",  "color" },
	    { "coller", "color" },
	    { "colour", "color" },

	    // colors
	    { "colors",   "colors" },
	    { "collars",  "colors" },
	    { "cullers",  "colors" },
	    { "covers",   "colors" },
	    { "color is", "colors" },
	    { "colours",  "colors" },

	    // default
	    { "default",   "default" },
	    { "the fault", "default" },
	    { "d-fault",   "default" },
	    { "de-fault",  "default" },
	    { "defalt",    "default" },
	    { "dee fault", "default" },

	    // dropin / drop in
	    { "dropin",   "dropin" },
	    { "drop in",  "dropin" },
	    { "droppin",  "dropin" },
	    { "drop ink", "dropin" },
	    { "robin",    "dropin" },
	    { "draw pin", "dropin" },
	    { "dropping", "dropin" },

	    // effect
	    { "effect",  "effect" },
	    { "affect",  "effect" },
	    { "infect",  "effect" },
	    { "effec",   "effect" },
	    { "affec",   "effect" },
	    { "in fact", "effect" },

	    // effects
	    { "effects",   "effects" },
	    { "affects",   "effects" },
	    { "infects",   "effects" },
	    { "effect is", "effects" },

	    // explode
	    { "explode",    "explode" },
	    { "expload",    "explode" },
	    { "egg splode", "explode" },
	    { "exploaded",  "explode" },
	    { "implode",    "explode" },
	    
	    // focus
	    { "focus",     "focus" },
	    { "folks is",  "focus" },
	    { "folks us",  "focus" },
	    { "phocus",    "focus" },
	    { "focas",     "focus" },
	    { "fokus",     "focus" },
	    { "faux kiss", "focus" },
	    { "full kiss", "focus" },
	    { "hocus",     "focus" },
	    { "locust",    "focus" },

	    // giveaway
	    { "giveaway",   "giveaway" },
	    { "give away",  "giveaway" },
	    { "give a way", "giveaway" },
	    { "gave away",  "giveaway" },
	    { "give way",   "giveaway" },

	    // hydrate
	    { "hydrate",    "hydrate" },
	    { "high drate", "hydrate" },
	    { "high trait", "hydrate" },
	    { "hydrated",   "hydrate" },
	    { "eye drate",  "hydrate" },
	    { "hi drate",   "hydrate" },

	    // jump
	    { "jump",   "jump" },
	    { "jumped", "jump" },
	    { "chump",  "jump" },
	    { "gump",   "jump" },
	    { "dump",   "jump" },
	    { "ump",    "jump" },
	    { "junk",   "jump" },

	    // kill
	    { "kill",   "kill" },
	    { "killed", "kill" },
	    { "keel",   "kill" },
	    { "hill",   "kill" },
	    { "will",   "kill" },
	    { "kohl",   "kill" },
	    { "gill",   "kill" },

	    // layout
	    { "layout",    "layout" },
	    { "lay out",   "layout" },
	    { "way out",   "layout" },
	    { "layer out", "layout" },
	    { "layed out", "layout" },

	    // lights
	    { "lights", "lights" },
	    { "light",  "lights" },
	    { "likes",  "lights" },
	    { "nights", "lights" },
	    { "rights", "lights" },
	    { "whites", "lights" },
	    { "ites",   "lights" },
	    
	    // main
	    { "main",  "main" },
	    { "maine", "main" },
	    { "mane",  "main" },
	    { "mean",  "main" },
	    { "mine",  "main" },
	    { "mame",  "main" },
	    { "man",   "main" },

	    // me
	    { "me",   "me" },
	    { "be",   "me" },
	    { "we",   "me" },
	    { "my",   "me" },
	    { "knee", "me" },
	    { "see",  "me" },
	    { "the",  "me" },
	    
	    // microphone
	    { "mic",            "mic" },
	    { "mike",           "mic" },
	    { "might",          "mic" },
	    { "night",          "mic" },
	    { "tight",          "mic" },
	    { "tyke",           "mic" },
	    { "microphone",     "mic" },
	    { "micro phone",    "mic" },
	    { "might of phone", "mic" },
	    { "nitro phone",    "mic" },
	    
	    // model
	    { "model",  "model" },
	    { "mottle", "model" },
	    { "modle",  "model" },
	    { "modal",  "model" },
	    { "module", "model" },
	    { "muddle", "model" },
	    { "metal",  "model" },

	    // name
	    { "name",  "name" },
	    { "neigh", "name" },
	    { "naim",  "name" },
	    { "neme",  "name" },
	    { "game",  "name" },
	    { "tame",  "name" },

	    // not safe for work
	    { "not safe for work", "nsfw" },
	    { "nsfw",              "nsfw" },
	    { "not safe work",     "nsfw" },
	    { "nutshell for work", "nsfw" },
	    { "not safe for look", "nsfw" },
	    { "not save for work", "nsfw" },
	    { "not say for work",  "nsfw" },
	    
	    // obs
	    { "obs",      "obs" },
	    { "o b s",    "obs" },
	    { "o be s",   "obs" },
	    { "o bee s",  "obs" },
	    { "oh b s",   "obs" },
	    { "oh be s",  "obs" },
	    { "oh bee s", "obs" },

	    // outline
	    { "outline",    "outline" },
	    { "out line",   "outline" },
	    { "about line", "outline" },
	    { "out lying",  "outline" },
	    { "outlyne",    "outline" },
	    { "out lane",   "outline" },
	    { "alright",    "outline" },

	    // paper!obs
	    { "paper",   "paper" },
	    { "pay per", "paper" },
	    { "papper",  "paper" },
	    { "piper",   "paper" },
	    { "vapor",   "paper" },
	    { "payor",   "paper" },
	    { "taper",   "paper" },

	    // preview
	    { "preview",  "preview" },
	    { "pre view", "preview" },
	    { "privew",   "preview" },
	    { "pervew",   "preview" },
	    { "purview",  "preview" },
	    { "pre-view", "preview" },
	    { "freeview", "preview" },
	    
	    // record
	    { "record",    "record" },
	    { "recording", "record" },
	    { "records",   "record" },
	    { "re cord",   "record" },
	    { "re chord",  "record" },

	    // repeat
	    { "re peat",    "repeat" },
	    { "repeat",     "repeat" },
	    { "repeating",  "repeat" },
	    { "repeats",    "repeat" },
	    { "repel",      "repeat" },
	    { "reprieve",   "repeat" },
	    { "reprieves",  "repeat" },
	    { "retreat",    "repeat" },
	    { "retreats",   "repeat" },
	    { "retreating", "repeat" },
	    { "teepee",     "repeat" },
	    
	    // reset
	    { "reset",     "reset" },
	    { "re set",    "reset" },
	    { "re-set",    "reset" },
	    { "re sit",    "reset" },
	    { "receipt",   "reset" },
	    { "recent",    "reset" },
	    { "resetting", "reset" },

	    // rock
	    { "rock",  "rock" },
	    { "rawk",  "rock" },
	    { "roc",   "rock" },
	    { "brock", "rock" },
	    { "lock",  "rock" },
	    { "rack",  "rock" },

	    // roll the dice
	    { "roll the dice", "rtd" },
	    { "roll dice",     "rtd" },
	    { "roll the dies", "rtd" },
	    { "rtd",           "rtd" },
	    { "roll the dose", "rtd" },
	    { "roll the nice", "rtd" },
	    { "role the dice", "rtd" },

	    // scissors
	    { "scissors", "scissors" },
	    { "scissers", "scissors" },
	    { "sizzors",  "scissors" },
	    { "sisters",  "scissors" },
	    { "scis",     "scissors" },
	    { "sizzers",  "scissors" },
	    { "sizors",   "scissors" },

	    // services
	    { "service",   "services" },
	    { "services",  "services" },
	    { "serving",   "services" },
	    { "servis",    "services" },
	    { "serviced",  "services" },
	    { "servises",  "services" },
	    { "ser-vices", "services" },
	    { "surface",   "services" },
	    { "surfaces",  "services" },

	    // set
	    { "set",  "set" },
	    { "sut",  "set" },
	    { "sat",  "set" },
	    { "sent", "set" },
	    { "said", "set" },
	    { "sect", "set" },

	    // shader0
	    { "shader0",      "shader0" },
	    { "shader 0",     "shader0" },
	    { "shader zero",  "shader0" },
	    { "shatter zero", "shader0" },
	    { "shader oh",    "shader0" },
	    { "shader low",   "shader0" },
	    { "shutter zero", "shader0" },

	    // shader1
	    { "shader1",     "shader1" },
	    { "shader 1",    "shader1" },
	    { "shader one",  "shader1" },
	    { "shatter one", "shader1" },
	    { "shader won",  "shader1" },
	    { "shutter one", "shader1" },
	    { "shader when", "shader1" },

	    // shader2
	    { "shader2",     "shader2" },
	    { "shader 2",    "shader2" },
	    { "shader two",  "shader2" },
	    { "shatter two", "shader2" },
	    { "shader to",   "shader2" },
	    { "shader too",  "shader2" },
	    { "shutter two", "shader2" },

	    // shader3
	    { "shader3",       "shader3" },
	    { "shader 3",      "shader3" },
	    { "shader three",  "shader3" },
	    { "shatter three", "shader3" },
	    { "shader tree",   "shader3" },
	    { "shutter three", "shader3" },
	    { "shader free",   "shader3" },

	    // skip
	    { "skip",     "skip" },
	    { "skipping", "skip" },
	    { "skips",    "skip" },
	    { "skit",     "skip" },
	    { "skin",     "skip" },
	    { "skiped",   "skip" },
	    { "scip",     "skip" },
	    { "skip it",  "skip" },
	    { "ship",     "skip" },

	    // song
	    { "song",   "song" },
	    { "songs",  "song" },
	    { "sung",   "song" },
	    { "long",   "song" },
	    { "strong", "song" },
	    { "thong",  "song" },
	    { "sang",   "song" },
	    { "some",   "song" },

	    // sound effect
	    { "sound effect",    "sfx" },
	    { "sound effects",   "sfx" },
	    { "sound infect",    "sfx" },
	    { "sound affect",    "sfx" },
	    { "sound a fect",    "sfx" },
	    { "sound effect is", "sfx" },
	    { "sound effective", "sfx" },
	    
	    // start
	    { "start",    "start" },
	    { "starts",   "start" },
	    { "starting", "start" },
	    { "stark",    "start" },
	    { "stat",     "start" },
	    { "stort",    "start" },
	    { "smart",    "start" },
	    { "tart",     "start" },

	    // stop
	    { "shop",     "stop" },
	    { "sop",      "stop" },
	    { "step",     "stop" },
	    { "stock",    "stop" },
	    { "stop",     "stop" },
	    { "stopp",    "stop" },
	    { "stops",    "stop" },
	    { "stopping", "stop" },
	    { "top",      "stop" },
	    
	    // stream
	    { "beam",   "stream" },
	    { "cream",  "stream" },
	    { "ream",   "stream" },
	    { "scream", "stream" },
	    { "steam",  "stream" },
	    { "stream", "stream" },
	    { "team",   "stream" },

	    // stretch
	    { "stretch", "stretch" },
	    { "strech",  "stretch" },
	    { "stretsh", "stretch" },
	    { "stritch", "stretch" },
	    { "sketch",  "stretch" },
	    { "stratch", "stretch" },
	    { "tretch",  "stretch" },

	    // taunt
	    { "daunt",  "taunt" },
	    { "haunt",  "taunt" },
	    { "taught", "taunt" },
	    { "taunt",  "taunt" },
	    { "taut",   "taunt" },
	    { "tont",   "taunt" },

	    // team fortress 2
	    { "team fortress 2",   "tf2" },
	    { "team fortress two", "tf2" },
	    { "tf2",               "tf2" },
	    { "t f 2",             "tf2" },
	    { "teen fortress",     "tf2" },
	    { "team fortress too", "tf2" },
	    { "team fortress to",  "tf2" },

	    // test
	    { "test",   "test" },
	    { "tost",   "test" },
	    { "tist",   "test" },
	    { "tessed", "test" },
	    { "best",   "test" },
	    { "west",   "test" },
	    { "rest",   "test" },

	    // title
	    { "tidal",   "title" },
	    { "tidle",   "title" },
	    { "tightal", "title" },
	    { "tightel", "title" },
	    { "tital",   "title" },
	    { "tite",    "title" },
	    { "title",   "title" },

	    // titles
	    { "titles",    "titles" },
	    { "tidals",    "titles" },
	    { "titals",    "titles" },
	    { "tights",    "titles" },
	    { "titles is", "titles" },
	    { "tidles",    "titles" },
	    { "titels",    "titles" },

	    // unlock
	    { "an luck",   "unlock" },
	    { "on lock",   "unlock" },
	    { "on luck",   "unlock" },
	    { "un-lock",   "unlock" },
	    { "unlock",    "unlock" },
	    { "unlocked",  "unlock" },
	    { "unlocking", "unlock" },
	    { "unlocks",   "unlock" },
	    { "unluck",    "unlock" },

	    // walk
	    { "walk",    "walk" },
	    { "walks",   "walk" },
	    { "walking", "walk" },
	    { "wok",     "walk" },
	    { "wall",    "walk" },
	    { "work",    "walk" },
	    { "woke",    "walk" },
	    { "walked",  "walk" },

	    // workout
	    { "workout",    "workout" },
	    { "work out",   "workout" },
	    { "walk out",   "workout" },
	    { "work it",    "workout" },
	    { "war out",    "workout" },
	    { "ware out",   "workout" },
	    { "worked out", "workout" },
	    
	    #endregion
	    
	    #region Colors
	    
	    { "blue",    "blue" },
	    { "blew",    "blue" },
	    { "boo",     "blue" },
	    { "glue",    "blue" },
	    { "clue",    "blue" },
	    { "bloo",    "blue" },
	    { "blue is", "blue" },

	    // blue raspberry
	    { "blue raspberry",  "blue raspberry" },
	    { "blue raz berry",  "blue raspberry" },
	    { "blue rasberry",   "blue raspberry" },
	    { "blue razz",       "blue raspberry" },
	    { "blue razz berry", "blue raspberry" },
	    { "blew raspberry",  "blue raspberry" },
	    { "blue brass berry", "blue raspberry" },

	    // creamsicle banana
	    { "creamsicle banana",   "creamsicle banana" },
	    { "cream sickle banana", "creamsicle banana" },
	    { "cream cycle banana",  "creamsicle banana" },
	    { "green sickle banana", "creamsicle banana" },
	    { "creamsicle nana",     "creamsicle banana" },
	    { "cream circle banana", "creamsicle banana" },
	    { "creamsicle bonanza",  "creamsicle banana" },

	    // creamsicle blueberry
	    { "creamsicle blueberry",   "creamsicle blueberry" },
	    { "cream sickle blueberry", "creamsicle blueberry" },
	    { "cream cycle blueberry",  "creamsicle blueberry" },
	    { "green sickle blueberry", "creamsicle blueberry" },
	    { "creamsicle blue berry",  "creamsicle blueberry" },
	    { "creamsicle blew berry",  "creamsicle blueberry" },
	    { "cream circle blueberry", "creamsicle blueberry" },

	    // creamsicle dragonfruit
	    { "creamsicle dragonfruit",    "creamsicle dragonfruit" },
	    { "cream sickle dragonfruit",  "creamsicle dragonfruit" },
	    { "cream cycle dragonfruit",   "creamsicle dragonfruit" },
	    { "creamsicle dragon fruit",   "creamsicle dragonfruit" },
	    { "creamsicle drag and fruit", "creamsicle dragonfruit" },
	    { "cream circle dragonfruit",  "creamsicle dragonfruit" },
	    { "green sickle dragonfruit",  "creamsicle dragonfruit" },

	    // creamsicle lime
	    { "creamsicle lime",   "creamsicle lime" },
	    { "cream sickle lime", "creamsicle lime" },
	    { "cream cycle lime",  "creamsicle lime" },
	    { "creamsicle line",   "creamsicle lime" },
	    { "creamsicle rhyme",  "creamsicle lime" },
	    { "cream circle lime", "creamsicle lime" },
	    { "green sickle lime", "creamsicle lime" },

	    // creamsicle orange
	    { "creamsicle orange",   "creamsicle orange" },
	    { "cream sickle orange", "creamsicle orange" },
	    { "cream cycle orange",  "creamsicle orange" },
	    { "creamsicle ornge",    "creamsicle orange" },
	    { "creamsicle range",    "creamsicle orange" },
	    { "cream circle orange", "creamsicle orange" },
	    { "green sickle orange", "creamsicle orange" },

	    // creamsicle strawberry
	    { "creamsicle strawberry",   "creamsicle strawberry" },
	    { "cream sickle strawberry", "creamsicle strawberry" },
	    { "cream cycle strawberry",  "creamsicle strawberry" },
	    { "creamsicle straw berry",  "creamsicle strawberry" },
	    { "creamsicle draw berry",   "creamsicle strawberry" },
	    { "cream circle strawberry", "creamsicle strawberry" },
	    { "green sickle strawberry", "creamsicle strawberry" },

	    // cyan
	    { "cyan",     "cyan" },
	    { "scion",    "cyan" },
	    { "sigh ann", "cyan" },
	    { "science",  "cyan" },
	    { "sy ann",   "cyan" },
	    { "si ann",   "cyan" },
	    { "cyann",    "cyan" },

	    // cyberpunk
	    { "cyberpunk",     "cyberpunk" },
	    { "cyber punk",    "cyberpunk" },
	    { "cypher punk",   "cyberpunk" },
	    { "sabre punk",    "cyberpunk" },
	    { "cyber bunk",    "cyberpunk" },
	    { "cyber monk",    "cyberpunk" },
	    { "sipped a punk", "cyberpunk" },

	    // forest sunset
	    { "forest sunset",   "forest sunset" },
	    { "forrest sunset",  "forest sunset" },
	    { "forest sun set",  "forest sunset" },
	    { "forced sunset",   "forest sunset" },
	    { "forest some set", "forest sunset" },
	    { "forest sunsit",   "forest sunset" },
	    { "forest son set",  "forest sunset" },

	    // green
	    { "green", "green" },
	    { "grin",  "green" },
	    { "grean", "green" },
	    { "keen",  "green" },
	    { "clean", "green" },
	    { "creen", "green" },

	    // heatwave
	    { "heatwave",   "heatwave" },
	    { "heat wave",  "heatwave" },
	    { "heatweave",  "heatwave" },
	    { "heet wave",  "heatwave" },
	    { "eat wave",   "heatwave" },
	    { "heat wayne", "heatwave" },
	    { "feet wave",  "heatwave" },

	    // icy
	    { "icy",     "icy" },
	    { "icey",    "icy" },
	    { "i see",   "icy" },
	    { "eye see", "icy" },
	    { "i sea",   "icy" },
	    { "ice see", "icy" },
	    { "icy is",  "icy" },

	    // magenta
	    { "magenta",   "magenta" },
	    { "my gentle", "magenta" },
	    { "magent",    "magenta" },
	    { "majenta",   "magenta" },
	    { "my genta",  "magenta" },
	    { "ma gentle", "magenta" },
	    { "muh genta", "magenta" },

	    // orange
	    { "orange",  "orange" },
	    { "ornge",   "orange" },
	    { "range",   "orange" },
	    { "arenge",  "orange" },
	    { "a range", "orange" },
	    { "oringe",  "orange" },
	    { "arrnge",  "orange" },

	    // orange purple
	    { "orange purple", "orange purple" },
	    { "range purple",  "orange purple" },
	    { "orange perple", "orange purple" },
	    { "orange purp",   "orange purple" },
	    { "ornge purple",  "orange purple" },
	    { "orange people", "orange purple" },

	    // purple
	    { "purple", "purple" },
	    { "perple", "purple" },
	    { "purp",   "purple" },
	    { "people", "purple" },
	    { "purpel", "purple" },
	    { "burple", "purple" },
	    { "urple",  "purple" },

	    // rainbow
	    { "rainbow",   "rainbow" },
	    { "rain bow",  "rainbow" },
	    { "rane bow",  "rainbow" },
	    { "rainbeau",  "rainbow" },
	    { "reign bow", "rainbow" },
	    { "rain low",  "rainbow" },
	    { "rain go",   "rainbow" },

	    // red
	    { "red",  "red" },
	    { "read", "red" },
	    { "rid",  "red" },
	    { "fed",  "red" },
	    { "head", "red" },
	    { "rad",  "red" },

	    // red white blue
	    { "red white blue",     "red white blue" },
	    { "red white blew",     "red white blue" },
	    { "read white blue",    "red white blue" },
	    { "red white and blue", "red white blue" },
	    { "rad white blue",     "red white blue" },
	    { "red light blue",     "red white blue" },
	    { "red white boo",      "red white blue" },

	    // toxic
	    { "toxic",     "toxic" },
	    { "toxit",     "toxic" },
	    { "tocks it",  "toxic" },
	    { "tock sick", "toxic" },
	    { "toxick",    "toxic" },
	    { "talk sick", "toxic" },
	    { "dock sick", "toxic" },

	    // vaporwave
	    { "vaporwave",   "vaporwave" },
	    { "vapor wave",  "vaporwave" },
	    { "vaper wave",  "vaporwave" },
	    { "vaperwave",   "vaporwave" },
	    { "paper wave",  "vaporwave" },
	    { "favour wave", "vaporwave" },
	    { "vape or wave", "vaporwave" },

	    // watermelon
	    { "watermelon",     "watermelon" },
	    { "water melon",    "watermelon" },
	    { "watt are melon", "watermelon" },
	    { "water mellen",   "watermelon" },
	    { "water million",  "watermelon" },
	    { "watter melon",   "watermelon" },
	    { "water mailing",  "watermelon" },

	    // white
	    { "white",  "white" },
	    { "wight",  "white" },
	    { "quite",  "white" },
	    { "ight",   "white" },
	    { "height", "white" },
	    { "whyte",  "white" },

	    // yellow
	    { "yellow",  "yellow" },
	    { "yello",   "yellow" },
	    { "yallow",  "yellow" },
	    { "hellow",  "yellow" },
	    { "fellow",  "yellow" },
	    { "mellow",  "yellow" },
	    { "yell oh", "yellow" },
	    
	    #endregion
	    
	    #region Effects
	    
	    { "atomic particles",      "atomic particles" },
	    { "a tomic particles",     "atomic particles" },
	    { "atomic potticles",      "atomic particles" },
	    { "atomic articles",       "atomic particles" },
	    { "atomic particals",      "atomic particles" },
	    { "a tomic part of calls", "atomic particles" },
	    { "atomic radicals",       "atomic particles" },

	    // binary rain
	    { "binary rain",  "binary rain" },
	    { "binery rain",  "binary rain" },
	    { "finery rain",  "binary rain" },
	    { "bindery rain", "binary rain" },
	    { "binary reign", "binary rain" },
	    { "bi-nary rain", "binary rain" },

	    // bio sparks
	    { "bio sparks",    "bio sparks" },
	    { "biosparks",     "bio sparks" },
	    { "bi-oh sparks",  "bio sparks" },
	    { "bio barks",     "bio sparks" },
	    { "buy oh sparks", "bio sparks" },
	    { "bio parks",     "bio sparks" },
	    { "bio shocks",    "bio sparks" },

	    // cellular armor
	    { "cellular armor",   "cellular armor" },
	    { "sell u lar armor", "cellular armor" },
	    { "settler armor",    "cellular armor" },
	    { "cellular armour",  "cellular armor" },
	    { "sell ular armor",  "cellular armor" },
	    { "cellular armer",   "cellular armor" },
	    { "stellar armor",    "cellular armor" },

	    // circuit flow
	    { "circuit flow",    "circuit flow" },
	    { "search it flow",  "circuit flow" },
	    { "circut flow",     "circuit flow" },
	    { "circuit floe",    "circuit flow" },
	    { "circuit blow",    "circuit flow" },
	    { "circulate flow",  "circuit flow" },
	    { "surkit flow",     "circuit flow" },

	    // clockwise sweep
	    { "clockwise sweep",  "clockwise sweep" },
	    { "clock wise sweep", "clockwise sweep" },
	    { "glock wise sweep", "clockwise sweep" },
	    { "clockwise sleep",  "clockwise sweep" },
	    { "clockwise weep",   "clockwise sweep" },
	    { "clock wise suite", "clockwise sweep" },
	    { "clockwise sweet",  "clockwise sweep" },

	    // data stream
	    { "data stream",    "data stream" },
	    { "dayta stream",   "data stream" },
	    { "dadda stream",   "data stream" },
	    { "data scream",    "data stream" },
	    { "data steam",     "data stream" },
	    { "data extreme",   "data stream" },
	    { "day ta stream",  "data stream" },

	    // diagonal rain
	    { "diagonal rain",   "diagonal rain" },
	    { "die agonal rain", "diagonal rain" },
	    { "diagonal reign",  "diagonal rain" },
	    { "diagnal rain",    "diagonal rain" },
	    { "dye agonal rain", "diagonal rain" },
	    { "diagonal lane",   "diagonal rain" },

	    // diamond pulse
	    { "diamond pulse",  "diamond pulse" },
	    { "dimond pulse",   "diamond pulse" },
	    { "diamond pulls",  "diamond pulse" },
	    { "diamond pole",   "diamond pulse" },
	    { "diamond pause",  "diamond pulse" },
	    { "die mond pulse", "diamond pulse" },
	    { "dimon pulse",    "diamond pulse" },

	    // dot matrix
	    { "dot matrix",    "dot matrix" },
	    { "dot matrics",   "dot matrix" },
	    { "dot mat ricks", "dot matrix" },
	    { "dark matrix",   "dot matrix" },
	    { "dot may trix",  "dot matrix" },
	    { "dot mattress",  "dot matrix" },

	    // electric cracks
	    { "electric cracks",  "electric cracks" },
	    { "e-lectric cracks", "electric cracks" },
	    { "electric crax",    "electric cracks" },
	    { "electric racks",   "electric cracks" },
	    { "e lectric cracks", "electric cracks" },
	    { "electric tracks",  "electric cracks" },
	    { "alectric cracks",  "electric cracks" },

	    // energy scan
	    { "energy scan",    "energy scan" },
	    { "inner gee scan", "energy scan" },
	    { "energy skin",    "energy scan" },
	    { "energy span",    "energy scan" },
	    { "energy scam",    "energy scan" },
	    { "n-ergy scan",    "energy scan" },
	    { "en-ergy scan",   "energy scan" },

	    // falling shards
	    { "falling shards", "falling shards" },
	    { "falling sharks", "falling shards" },
	    { "falling cards",  "falling shards" },
	    { "falling yards",  "falling shards" },
	    { "balling shards", "falling shards" },
	    { "falling shots",  "falling shards" },

	    // fractal zoom
	    { "fractal zoom",  "fractal zoom" },
	    { "fractel zoom",  "fractal zoom" },
	    { "fractor zoom",  "fractal zoom" },
	    { "fractal room",  "fractal zoom" },
	    { "frac tal zoom", "fractal zoom" },
	    { "fragile zoom",  "fractal zoom" },
	    { "fractal doom",  "fractal zoom" },

	    // glitch slices
	    { "glitch slices", "glitch slices" },
	    { "glitch lyces",  "glitch slices" },
	    { "glitch iso",    "glitch slices" },
	    { "glitch lices",  "glitch slices" },
	    { "glich slices",  "glitch slices" },
	    { "glitch prices", "glitch slices" },
	    { "glitch sizes",  "glitch slices" },

	    // hex shield
	    { "hex shield",   "hex shield" },
	    { "heck shield",  "hex shield" },
	    { "heck sheeled", "hex shield" },
	    { "hex field",    "hex shield" },
	    { "hex healed",   "hex shield" },
	    { "x shield",     "hex shield" },

	    // honeycomb pulse
	    { "honeycomb pulse",  "honeycomb pulse" },
	    { "honey comb pulse", "honeycomb pulse" },
	    { "honeycomb pulls",  "honeycomb pulse" },
	    { "money comb pulse", "honeycomb pulse" },
	    { "honeycomb pole",   "honeycomb pulse" },
	    { "honey comb pole",  "honeycomb pulse" },
	    { "honeycomb pause",  "honeycomb pulse" },

	    // interference bars
	    { "interference bars",    "interference bars" },
	    { "inner ference bars",   "interference bars" },
	    { "inter-ference bars",   "interference bars" },
	    { "interference stars",   "interference bars" },
	    { "interference cars",    "interference bars" },
	    { "inter ference bars",   "interference bars" },
	    { "inner fear ence bars", "interference bars" },

	    // laser sweep
	    { "laser sweep",   "laser sweep" },
	    { "lazer sweep",   "laser sweep" },
	    { "laser sleep",   "laser sweep" },
	    { "laser weep",    "laser sweep" },
	    { "laser suite",   "laser sweep" },
	    { "lay zer sweep", "laser sweep" },
	    { "lazy sweep",    "laser sweep" },

	    // lava bubbles
	    { "lava bubbles",  "lava bubbles" },
	    { "larva bubbles", "lava bubbles" },
	    { "lava doubles",  "lava bubbles" },
	    { "lava bubs",     "lava bubbles" },
	    { "lava puddles",  "lava bubbles" },
	    { "la va bubbles", "lava bubbles" },

	    // matrix stripes
	    { "matrix stripes",    "matrix stripes" },
	    { "mat ricks stripes", "matrix stripes" },
	    { "matrix tripes",     "matrix stripes" },
	    { "matrix types",      "matrix stripes" },
	    { "may trix stripes",  "matrix stripes" },
	    { "matrix gripes",     "matrix stripes" },
	    { "matrix wipes",      "matrix stripes" },

	    // moire lines
	    { "moire lines",   "moire lines" },
	    { "mory lines",    "moire lines" },
	    { "mwah lines",    "moire lines" },
	    { "more lines",    "moire lines" },
	    { "mwahr lines",   "moire lines" },
	    { "moir lines",    "moire lines" },
	    { "moy ray lines", "moire lines" },

	    // moving arcs
	    { "moving arcs",  "moving arcs" },
	    { "moving arks",  "moving arcs" },
	    { "moving arts",  "moving arcs" },
	    { "moving ax",    "moving arcs" },
	    { "moving darks", "moving arcs" },
	    { "moving acts",  "moving arcs" },
	    { "mooving arcs", "moving arcs" },

	    // neural flash
	    { "neural flash",  "neural flash" },
	    { "nural flash",   "neural flash" },
	    { "neural flesh",  "neural flash" },
	    { "neural lash",   "neural flash" },
	    { "new rel flash", "neural flash" },
	    { "neuro flash",   "neural flash" },
	    { "neural clash",  "neural flash" },

	    // outward ripples
	    { "outward ripples",    "outward ripples" },
	    { "out ward ripples",   "outward ripples" },
	    { "outward triples",    "outward ripples" },
	    { "outward nipples",    "outward ripples" },
	    { "outward riddles",    "outward ripples" },
	    { "out ward rip pulls", "outward ripples" },
	    { "outward rippels",    "outward ripples" },

	    // overdrive bars
	    { "overdrive bars",  "overdrive bars" },
	    { "over drive bars", "overdrive bars" },
	    { "overdrive stars", "overdrive bars" },
	    { "overdrive cars",  "overdrive bars" },
	    { "over-drive bars", "overdrive bars" },
	    { "overly bars",     "overdrive bars" },

	    // plasma drift
	    { "plasma drift",  "plasma drift" },
	    { "plazma drift",  "plasma drift" },
	    { "plasma gift",   "plasma drift" },
	    { "plasma draft",  "plasma drift" },
	    { "plasma lift",   "plasma drift" },
	    { "plaz ma drift", "plasma drift" },
	    { "plasma rift",   "plasma drift" },

	    // power grid
	    { "power grid",  "power grid" },
	    { "pow er grid", "power grid" },
	    { "power grit",  "power grid" },
	    { "power greed", "power grid" },
	    { "power bread", "power grid" },
	    { "tower grid",  "power grid" },

	    // pulse wave
	    { "pulse wave",  "pulse wave" },
	    { "pulls wave",  "pulse wave" },
	    { "pole wave",   "pulse wave" },
	    { "pulse way",   "pulse wave" },
	    { "pause wave",  "pulse wave" },
	    { "pulse weigh", "pulse wave" },
	    { "puls wave",   "pulse wave" },

	    // radar ring
	    { "radar ring",   "radar ring" },
	    { "ray dar ring", "radar ring" },
	    { "radar wing",   "radar ring" },
	    { "radar king",   "radar ring" },
	    { "radar bring",  "radar ring" },
	    { "raider ring",  "radar ring" },
	    { "radar thing",  "radar ring" },

	    // rgb shift
	    { "rgb shift",   "rgb shift" },
	    { "r g b shift", "rgb shift" },
	    { "rgb drift",   "rgb shift" },
	    { "r g b rift",  "rgb shift" },
	    { "r d b shift", "rgb shift" },

	    // rolling magma
	    { "rolling magma", "rolling magma" },
	    { "rowing magma",  "rolling magma" },
	    { "rolling mama",  "rolling magma" },
	    { "rolling magna", "rolling magma" },

	    // rotating cubes
	    { "rotating cubes",   "rotating cubes" },
	    { "row tating cubes", "rotating cubes" },
	    { "rotating tubes",   "rotating cubes" },
	    { "rotating pubes",   "rotating cubes" },
	    { "rotating boobs",   "rotating cubes" },
	    { "row tating tubes", "rotating cubes" },

	    // scrolling vines
	    { "scrolling vines", "scrolling vines" },
	    { "scrolling lines", "scrolling vines" },
	    { "scrolling signs", "scrolling vines" },
	    { "scrolling pines", "scrolling vines" },
	    { "skrolling vines", "scrolling vines" },
	    { "scrolling finds", "scrolling vines" },
	    { "scrolling binds", "scrolling vines" },

	    // silk threads
	    { "silk threads", "silk threads" },
	    { "silk dreads",  "silk threads" },
	    { "silk shreds",  "silk threads" },
	    { "silk heads",   "silk threads" },
	    { "soak threads", "silk threads" },
	    { "silk treads",  "silk threads" },

	    // solar orbits
	    { "solar orbits",  "solar orbits" },
	    { "soler orbits",  "solar orbits" },
	    { "solar or bits", "solar orbits" },
	    { "solar orders",  "solar orbits" },
	    { "solar objects", "solar orbits" },
	    { "so lar orbits", "solar orbits" },
	    { "solar audits",  "solar orbits" },

	    // sonar pings
	    { "sonar pings",  "sonar pings" },
	    { "so nar pings", "sonar pings" },
	    { "sonar rings",  "sonar pings" },
	    { "sonar things", "sonar pings" },
	    { "sonar kings",  "sonar pings" },
	    { "sooner pings", "sonar pings" },
	    { "sonar pinks",  "sonar pings" },

	    // spinning vortex
	    { "spinning vortex",  "spinning vortex" },
	    { "spinning fortex",  "spinning vortex" },
	    { "spinning vertex",  "spinning vortex" },
	    { "spinning wartex",  "spinning vortex" },
	    { "spinning boretex", "spinning vortex" },
	    { "spin ning vortex", "spinning vortex" },

	    // square tiling
	    { "square tiling",  "square tiling" },
	    { "square styling", "square tiling" },
	    { "square filing",  "square tiling" },
	    { "skware tiling",  "square tiling" },
	    { "square piling",  "square tiling" },

	    // static glitch
	    { "static glitch",     "static glitch" },
	    { "statically glitch", "static glitch" },
	    { "static flitch",     "static glitch" },
	    { "static witch",      "static glitch" },
	    { "static ditch",      "static glitch" },
	    { "stat it glitch",    "static glitch" },
	    { "static glich",      "static glitch" },

	    // swarming nanites
	    { "swarming nanites",  "swarming nanites" },
	    { "swarming nannys",   "swarming nanites" },
	    { "swarming midnight", "swarming nanites" },
	    { "swarming nan ites", "swarming nanites" },
	    { "swarming manites",  "swarming nanites" },
	    { "swarming knights",  "swarming nanites" },
	    { "swarming nites",    "swarming nanites" },

	    // topo lines
	    { "topo lines",          "topo lines" },
	    { "toe po lines",        "topo lines" },
	    { "top o lines",         "topo lines" },
	    { "total lines",         "topo lines" },
	    { "taupe oh lines",      "topo lines" },
	    { "topographical lines", "topo lines" },

	    // tracking lines
	    { "tracking lines", "tracking lines" },
	    { "tranking lines", "tracking lines" },
	    { "tracking signs", "tracking lines" },
	    { "tracking vines", "tracking lines" },
	    { "traching lines", "tracking lines" },
	    { "cracking lines", "tracking lines" },
	    { "tracking minds", "tracking lines" },

	    // tunnel starfield
	    { "tunnel starfield",   "tunnel starfield" },
	    { "tunnel star field",  "tunnel starfield" },
	    { "tunnel star-field",  "tunnel starfield" },
	    { "tunnel Garfield",    "tunnel starfield" },
	    { "tonnel starfield",   "tunnel starfield" },
	    { "tunnel start field", "tunnel starfield" },

	    // vapor grid
	    { "vapor grid",  "vapor grid" },
	    { "vaper grid",  "vapor grid" },
	    { "paper grid",  "vapor grid" },
	    { "vapor grit",  "vapor grid" },
	    { "vapor greed", "vapor grid" },
	    { "va per grid", "vapor grid" },
	    { "favour grid", "vapor grid" },

	    // vertical bitstream
	    { "vertical bitstream",  "vertical bitstream" },
	    { "vertical bit stream", "vertical bitstream" },
	    { "vertical midstream",  "vertical bitstream" },
	    { "vertical bit-stream", "vertical bitstream" },
	    { "vurtical bitstream",  "vertical bitstream" },
	    { "vertical fitstream",  "vertical bitstream" },

	    // vertical drift
	    { "vertical drift", "vertical drift" },
	    { "vertical draft", "vertical drift" },
	    { "vertical lift",  "vertical drift" },
	    { "vertical rift",  "vertical drift" },
	    { "vurtical drift", "vertical drift" },
	    { "vertical shift", "vertical drift" },

	    // wind streaks
	    { "wind streaks", "wind streaks" },
	    { "wind peaks",   "wind streaks" },
	    { "wind cheeks",  "wind streaks" },
	    { "wind seeks",   "wind streaks" },
	    { "win streaks",  "wind streaks" },
	    { "wind streets", "wind streaks" },

	    // zebra sweep
	    { "zebra sweep", "zebra sweep" },
	    { "zebra sleep", "zebra sweep" },
	    { "zebra weep",  "zebra sweep" },
	    { "zibra sweep", "zebra sweep" },
	    { "sebra sweep", "zebra sweep" },
	    
	    #endregion
	    
	    #region Emotes
	    
	    // bumper car
    	{ "bumper car",  "bumper car" },
    	{ "bumpercar",   "bumper car" },
    	{ "bumper core", "bumper car" },
    	{ "number car",  "bumper car" },
    	{ "bumper card", "bumper car" },
    	{ "pumper car",  "bumper car" },
    	{ "bumper bar",  "bumper car" },
	
    	// celebrate
    	{ "celebrate",       "celebrate" },
    	{ "celibate",        "celebrate" },
    	{ "celebrate sound", "celebrate" },
    	{ "seller brate",    "celebrate" },
    	{ "cell a brate",    "celebrate" },
    	{ "celebrating",     "celebrate" },
    	{ "sell a brate",    "celebrate" },
	
    	// conga
    	{ "conga",      "conga" },
    	{ "conga line", "conga" },
    	{ "konga",      "conga" },
    	{ "congo",      "conga" },
    	{ "longer",     "conga" },
    	{ "gonga",      "conga" },
    	{ "khonga",     "conga" },
	
    	// laugh
    	{ "laugh",       "laugh" },
    	{ "laughing",    "laugh" },
    	{ "laf",         "laugh" },
    	{ "laugh sound", "laugh" },
    	{ "off",         "laugh" },
    	{ "half",        "laugh" },
    	{ "staff",       "laugh" },
	
    	// random
    	{ "random",   "random" },
    	{ "randam",   "random" },
    	{ "ran dumb", "random" },
    	{ "randum",   "random" },
    	{ "randomly", "random" },
    	{ "phantom",  "random" },
    	{ "and them", "random" },
	
    	// rock paper scissors
    	{ "rock paper scissors", "rock paper scissors" },
    	{ "rock paper scissers", "rock paper scissors" },
    	{ "rock paper sizzors",  "rock paper scissors" },
    	{ "rock paper sister",   "rock paper scissors" },
    	{ "rock paper scis",     "rock paper scissors" },
    	{ "rock paper",          "rock paper scissors" },
    	{ "rps",                 "rock paper scissors" },
	
    	// sit
    	{ "sit",  "sit" },
    	{ "it",   "sit" },
    	{ "shit", "sit" },
    	{ "fit",  "sit" },
    	{ "lit",  "sit" },
	
    	// special1
    	{ "special1",       "special1" },
    	{ "special 1",      "special1" },
    	{ "special one",    "special1" },
    	{ "special won",    "special1" },
    	{ "special when",   "special1" },
    	{ "speshul one",    "special1" },
    	{ "specialist one", "special1" },
	
    	// special2
    	{ "special2",       "special2" },
    	{ "special 2",      "special2" },
    	{ "special two",    "special2" },
    	{ "special to",     "special2" },
    	{ "special too",    "special2" },
    	{ "speshul two",    "special2" },
    	{ "specialist two", "special2" },
	    
	    #endregion
	    
		#region Models
		
	    // human
	    { "human",    "human" },
	    { "hume in",  "human" },
	    { "hu man",   "human" },
	    { "you man",  "human" },
	    { "hue man",  "human" },
	    { "human is", "human" },
	    { "cumin",    "human" },

	    // airplane
	    { "airplane",   "airplane" },
	    { "air plane",  "airplane" },
	    { "err plane",  "airplane" },
	    { "air-plane",  "airplane" },
	    { "hair plane", "airplane" },
	    { "aeroplane",  "airplane" },
	    { "plain",      "airplane" },

	    // asteroid
	    { "asteroid",    "asteroid" },
	    { "asteroyd",    "asteroid" },
	    { "aster-oid",   "asteroid" },
	    { "assteroid",   "asteroid" },
	    { "asteroid is", "asteroid" },
	    { "astra id",    "asteroid" },
	    { "acid",        "asteroid" },

	    // banana
	    { "banana",    "banana" },
	    { "nana",      "banana" },
	    { "bananna",   "banana" },
	    { "bonanza",   "banana" },
	    { "buh nana",  "banana" },
	    { "banana is", "banana" },
	    { "panama",    "banana" },

	    // bone
	    { "bone",    "bone" },
	    { "boan",    "bone" },
	    { "own",     "bone" },
	    { "phone",   "bone" },
	    { "cone",    "bone" },
	    { "born",    "bone" },
	    { "boned",   "bone" },

	    // boobs
	    { "boobs",    "boobs" },
	    { "boobies",  "boobs" },
	    { "boobs is", "boobs" },
	    { "boots",    "boobs" },
	    { "boos",     "boobs" },

	    // brain
	    { "brain",    "brain" },
	    { "brane",    "brain" },
	    { "drain",    "brain" },
	    { "grain",    "brain" },
	    { "rain",     "brain" },
	    { "braine",   "brain" },
	    { "bray in",  "brain" },

	    // branch
	    { "branch",    "branch" },
	    { "branch is", "branch" },
	    { "bransh",    "branch" },
	    { "brancht",   "branch" },
	    { "ranch",     "branch" },
	    { "brantch",   "branch" },
	    { "brance",    "branch" },

	    // bread
	    { "bread",  "bread" },
	    { "bred",   "bread" },
	    { "tread",  "bread" },
	    { "breads", "bread" },

	    // cloud
	    { "cloud",    "cloud" },
	    { "clowd",    "cloud" },
	    { "clown",    "cloud" },
	    { "loud",     "cloud" },
	    { "clowed",   "cloud" },
	    { "cloud is", "cloud" },
	    { "proud",    "cloud" },

	    // companion cube
	    { "companion cube",    "companion cube" },
	    { "companyan cube",    "companion cube" },
	    { "companion tube",    "companion cube" },
	    { "company and cube",  "companion cube" },
	    { "compansion cube",   "companion cube" },
	    { "companion cube is", "companion cube" },
	    { "companion cute",    "companion cube" },
	    { "companion q",       "companion cube" },

	    // deep sea jellyfish
	    { "deep sea jellyfish",      "deep sea jellyfish" },
	    { "deep sea jelly fish",     "deep sea jellyfish" },
	    { "deep sea jelly",          "deep sea jellyfish" },
	    { "deep sea jelly wish",     "deep sea jellyfish" },
	    { "deep sea gentle fish",    "deep sea jellyfish" },
	    { "deep sea jelly dish",     "deep sea jellyfish" },
	    { "deep sea jelly fish is",  "deep sea jellyfish" },

	    // die
	    { "die",  "die" },
	    { "dye",  "die" },
	    { "dice", "die" },
	    { "day",  "die" },
	    { "tie",  "die" },
	    { "died", "die" },
	    { "bye",  "die" },

	    // dinosaur
	    { "dinosaur",    "dinosaur" },
	    { "dino",        "dinosaur" },
	    { "dina sore",   "dinosaur" },
	    { "dinosaur is", "dinosaur" },
	    { "dynasore",    "dinosaur" },
	    { "dino saur",   "dinosaur" },
	    { "dynosore",    "dinosaur" },

	    // donut
	    { "donut",     "donut" },
	    { "doughnut",  "donut" },
	    { "do nut",    "donut" },
	    { "doe nut",   "donut" },
	    { "do not",    "donut" },
	    { "donut is",  "donut" },
	    { "dough nut", "donut" },

	    // double helix
	    { "double helix",      "double helix" },
	    { "double healix",     "double helix" },
	    { "double heal licks", "double helix" },
	    { "bubble helix",      "double helix" },
	    { "double helix is",   "double helix" },
	    { "double he licks",   "double helix" },
	    { "double Felix",      "double helix" },

	    // dugtrio
	    { "dugtrio",      "dugtrio" },
	    { "dug trio",     "dugtrio" },
	    { "duck trio",    "dugtrio" },
	    { "dug tree oh",  "dugtrio" },
	    { "dugtreeo",     "dugtrio" },
	    { "dugtrio is",   "dugtrio" },
	    { "duck tree oh", "dugtrio" },

	    // egg
	    { "egg",    "egg" },
	    { "eg",     "egg" },
	    { "edge",   "egg" },
	    { "ag",     "egg" },
	    { "eggs",   "egg" },
	    { "egg is", "egg" },
	    { "eh",     "egg" },

	    // flask
	    { "flask",    "flask" },
	    { "flasks",   "flask" },
	    { "flas",     "flask" },
	    { "flast",    "flask" },
	    { "flack",    "flask" },
	    { "flask is", "flask" },
	    { "blast",    "flask" },

	    // frying pan
	    { "frying pan",    "frying pan" },
	    { "fry pan",       "frying pan" },
	    { "frying pen",    "frying pan" },
	    { "frying fan",    "frying pan" },
	    { "flying pan",    "frying pan" },
	    { "frying pan is", "frying pan" },
	    { "fry in pan",    "frying pan" },

	    // gears
	    { "gears",    "gears" },
	    { "gears is", "gears" },
	    { "geers",    "gears" },
	    { "years",    "gears" },
	    { "beers",    "gears" },
	    { "fears",    "gears" },

	    // ghost
	    { "ghost",    "ghost" },
	    { "ghosts",   "ghost" },
	    { "ghost is", "ghost" },
	    { "coast",    "ghost" },
	    { "post",     "ghost" },
	    { "goast",    "ghost" },
	    { "host",     "ghost" },

	    // glados
	    { "glados",    "glados" },
	    { "glad os",   "glados" },
	    { "gladdis",   "glados" },
	    { "glad us",   "glados" },
	    { "gladis",    "glados" },
	    { "glad-os",   "glados" },
	    { "glados is", "glados" },

	    // gun
	    { "gun",    "gun" },
	    { "done",   "gun" },
	    { "gone",   "gun" },
	    { "fun",    "gun" },
	    { "sun",    "gun" },
	    { "gun is", "gun" },
	    { "gon",    "gun" },

	    // hand
	    { "hand",    "hand" },
	    { "and",     "hand" },
	    { "band",    "hand" },
	    { "land",    "hand" },
	    { "hand is", "hand" },
	    { "hanned",  "hand" },
	    { "anned",   "hand" },

	    // hatsune miku
	    { "hatsune miku",     "hatsune miku" },
	    { "hot sune miku",    "hatsune miku" },
	    { "hat soon ay miku", "hatsune miku" },
	    { "hatsune me ku",    "hatsune miku" },
	    { "hatsune meko",     "hatsune miku" },
	    { "hatsune miku is",  "hatsune miku" },
	    { "hatsune micu",     "hatsune miku" },

	    // heart
	    { "heart",    "heart" },
	    { "hart",     "heart" },
	    { "hard",     "heart" },
	    { "art",      "heart" },
	    { "heart is", "heart" },
	    { "hert",     "heart" },

	    // jellyfish
	    { "jellyfish",     "jellyfish" },
	    { "jelly fish",    "jellyfish" },
	    { "jelly fish is", "jellyfish" },
	    { "jelly wish",    "jellyfish" },
	    { "jelly dish",    "jellyfish" },
	    { "gentle fish",   "jellyfish" },

	    // katana
	    { "katana",    "katana" },
	    { "ka tana",   "katana" },
	    { "ka-tana",   "katana" },
	    { "got tana",  "katana" },
	    { "katana is", "katana" },
	    { "cotana",    "katana" },
	    { "kitana",    "katana" },

	    // mushroom
	    { "mushroom",     "mushroom" },
	    { "mush room",    "mushroom" },
	    { "must room",    "mushroom" },
	    { "much room",    "mushroom" },
	    { "mush room is", "mushroom" },
	    { "mush-room",    "mushroom" },
	    { "mash room",    "mushroom" },

	    // octopus
	    { "octopus",    "octopus" },
	    { "octo pus",   "octopus" },
	    { "octo-pus",   "octopus" },
	    { "octapus",    "octopus" },
	    { "octopus is", "octopus" },
	    { "octo puss",  "octopus" },
	    { "ockto pus",  "octopus" },

	    // penis
	    { "penis",    "penis" },
	    { "pennys",   "penis" },
	    { "peenus",   "penis" },
	    { "penis is", "penis" },
	    { "peen",     "penis" },
	    { "pennice",  "penis" },

	    // pikmin
	    { "pikmin",    "pikmin" },
	    { "pick min",  "pikmin" },
	    { "pickman",   "pikmin" },
	    { "pick men",  "pikmin" },
	    { "pikman",    "pikmin" },
	    { "pikmin is", "pikmin" },
	    { "pick-min",  "pikmin" },

	    // pokeball
	    { "pokeball",     "pokeball" },
	    { "poke ball",    "pokeball" },
	    { "pokey ball",   "pokeball" },
	    { "poke-ball",    "pokeball" },
	    { "poke ball is", "pokeball" },
	    { "poca ball",    "pokeball" },

	    // potato
	    { "potato",    "potato" },
	    { "potatoes",  "potato" },
	    { "po tato",   "potato" },
	    { "po-tato",   "potato" },
	    { "potato is", "potato" },
	    { "potahto",   "potato" },
	    { "potayto",   "potato" },

	    // robot
	    { "robot",    "robot" },
	    { "row bot",  "robot" },
	    { "row-bot",  "robot" },
	    { "robot is", "robot" },
	    { "raw bot",  "robot" },
	    { "roebot",   "robot" },
	    { "roabut",   "robot" },

	    // rocket
	    { "rocket",    "rocket" },
	    { "raw ket",   "rocket" },
	    { "rock it",   "rocket" },
	    { "rocket is", "rocket" },
	    { "rock-it",   "rocket" },
	    { "raw-ket",   "rocket" },

	    // rook
	    { "rook",    "rook" },
	    { "root",    "rook" },
	    { "look",    "rook" },
	    { "brook",   "rook" },
	    { "rook is", "rook" },
	    { "rouke",   "rook" },

	    // sentry
	    { "sentry",    "sentry" },
	    { "century",   "sentry" },
	    { "centree",   "sentry" },
	    { "sentree",   "sentry" },
	    { "sentry is", "sentry" },
	    { "sen tree",  "sentry" },

	    // snowman
	    { "snowman",    "snowman" },
	    { "snow man",   "snowman" },
	    { "snow-man",   "snowman" },
	    { "snowman is", "snowman" },
	    { "no man",     "snowman" },
	    { "sno man",    "snowman" },

	    // solar system
	    { "solar system",    "solar system" },
	    { "soler system",    "solar system" },
	    { "soul are system", "solar system" },
	    { "solar system is", "solar system" },
	    { "solar-system",    "solar system" },
	    { "soler-system",    "solar system" },
	    { "solar sister",    "solar system" },

	    // spider
	    { "spider",    "spider" },
	    { "spyder",    "spider" },
	    { "spy der",   "spider" },
	    { "spider is", "spider" },
	    { "spy-der",   "spider" },
	    { "slider",    "spider" },
	    { "wider",     "spider" },

	    // squid
	    { "squid",    "squid" },
	    { "skwid",    "squid" },
	    { "skwed",    "squid" },
	    { "squid is", "squid" },
	    { "quid",     "squid" },
	    { "squad",    "squid" },
	    { "skid",     "squid" },

	    // star
	    { "star",    "star" },
	    { "starr",   "star" },
	    { "stare",   "star" },
	    { "star is", "star" },
	    { "stair",   "star" },
	    { "store",   "star" },

	    // sticky bomb
	    { "sticky bomb",    "sticky bomb" },
	    { "stick bomb",     "sticky bomb" },
	    { "sticky bomb is", "sticky bomb" },
	    { "sticky bum",     "sticky bomb" },
	    { "sticky mom",     "sticky bomb" },
	    { "sticky balm",    "sticky bomb" },
	    { "stickie bomb",   "sticky bomb" },

	    // tank
	    { "tank",    "tank" },
	    { "thank",   "tank" },
	    { "tank is", "tank" },
	    { "tanc",    "tank" },
	    { "bank",    "tank" },
	    { "sank",    "tank" },
	    { "dank",    "tank" },

	    // tie fighter
	    { "tie fighter",    "tie fighter" },
	    { "thai fighter",   "tie fighter" },
	    { "tye fighter",    "tie fighter" },
	    { "tie fighter is", "tie fighter" },
	    { "tie-fighter",    "tie fighter" },
	    { "thai-fighter",   "tie fighter" },
	    { "tight fighter",  "tie fighter" },

	    // tree
	    { "tree",    "tree" },
	    { "three",   "tree" },
	    { "free",    "tree" },
	    { "tree is", "tree" },
	    { "thee",    "tree" },
	    { "tre",     "tree" },
	    { "try",     "tree" },

	    // triangle
	    { "triangle",    "triangle" },
	    { "try angle",   "triangle" },
	    { "tri-angle",   "triangle" },
	    { "triangle is", "triangle" },
	    { "try-angle",   "triangle" },
	    { "tri angle",   "triangle" },
	    { "tryangle",    "triangle" },

	    // ufo
	    { "ufo",      "ufo" },
	    { "u f o",    "ufo" },
	    { "you f o",  "ufo" },
	    { "u f o is", "ufo" },
	    { "you-f-o",  "ufo" },
	    { "euphoria", "ufo" },

	    // xwing
	    { "xwing",     "xwing" },
	    { "x wing",    "xwing" },
	    { "x-wing",    "xwing" },
	    { "x wing is", "xwing" },
	    { "ex wing",   "xwing" },
	    { "acts wing", "xwing" },
	    
	    #endregion
		
		#region Sound Effects
		
		// applause
	    { "applause",    "applause" },
	    { "clapping",    "applause" },
	    { "applaws",     "applause" },
	    { "a pause",     "applause" },
	    { "apple laws",  "applause" },
	    { "up laws",     "applause" },
	    { "ap-plause",   "applause" },

	    // airhorn
	    { "airhorn",    "airhorn" },
	    { "air horn",   "airhorn" },
	    { "err horn",   "airhorn" },
	    { "airborn",    "airhorn" },
	    { "air thorn",  "airhorn" },
	    { "hair horn",  "airhorn" },
	    { "ear horn",   "airhorn" },

	    // ass
	    { "ass", "ass" },
	    { "as",  "ass" },
	    { "ash", "ass" },
	    { "has", "ass" },

	    // balls
	    { "balls", "balls" },
	    { "ballz", "balls" },
	    { "ball",  "balls" },
	    { "bowls", "balls" },
	    { "falls", "balls" },
	    { "walls", "balls" },
	    { "calls", "balls" },

	    // blink
	    { "blink",    "blink" },
	    { "blank",    "blink" },
	    { "blinked",  "blink" },
	    { "clink",    "blink" },
	    { "link",     "blink" },
	    { "blinking", "blink" },
	    { "plink",    "blink" },

	    // boing
	    { "boing",  "boing" },
	    { "boeing", "boing" },
	    { "boying", "boing" },
	    { "boring", "boing" },
	    { "going",  "boing" },
	    { "boink",  "boing" },
	    { "doing",  "boing" },

	    // bonk
	    { "bonk",   "bonk" },
	    { "bonked", "bonk" },
	    { "bunk",   "bonk" },
	    { "honk",   "bonk" },
	    { "monk",   "bonk" },
	    { "bong",   "bonk" },

	    // bruh
	    { "bruh",  "bruh" },
	    { "brah",  "bruh" },
	    { "bro",   "bruh" },
	    { "bru",   "bruh" },
	    { "brew",  "bruh" },
	    { "brush", "bruh" },
	    { "rough", "bruh" },

	    // buzzer
	    { "buzzer",   "buzzer" },
	    { "busier",   "buzzer" },
	    { "buzz",     "buzzer" },
	    { "fuzzer",   "buzzer" },
	    { "mussier",  "buzzer" },
	    { "buzz her", "buzzer" },
	    { "butter",   "buzzer" },

	    // censor
	    { "censor",  "censor" },
	    { "sensor",  "censor" },
	    { "senser",  "censor" },
	    { "center",  "censor" },
	    { "sender",  "censor" },
	    { "sincere", "censor" },
	    { "censure", "censor" },

	    // critical hit
	    { "critical hit",   "critical hit" },
	    { "crit hit",       "critical hit" },
	    { "crit",           "critical hit" },
	    { "critical bit",   "critical hit" },
	    { "critical sit",   "critical hit" },
	    { "critically hit", "critical hit" },
	    { "political hit",  "critical hit" },

	    // discord
	    { "discord",    "discord" },
	    { "this cord",  "discord" },
	    { "dischord",   "discord" },
	    { "disk cord",  "discord" },
	    { "dis-cord",   "discord" },
	    { "this chord", "discord" },
	    { "discored",   "discord" },

	    // dun dun
	    { "dun dun",   "dun dun" },
	    { "done done", "dun dun" },
	    { "dumb dumb", "dun dun" },
	    { "ton ton",   "dun dun" },
	    { "dun-dun",   "dun dun" },
	    { "sun sun",   "dun dun" },

	    // fart
	    { "fart",   "fart" },
	    { "farted", "fart" },
	    { "part",   "fart" },
	    { "fort",   "fart" },
	    { "dart",   "fart" },
	    { "bart",   "fart" },

	    // godlike
	    { "godlike",    "godlike" },
	    { "god like",   "godlike" },
	    { "gut like",   "godlike" },
	    { "godly",      "godlike" },
	    { "got like",   "godlike" },
	    { "god-like",   "godlike" },
	    { "guard like", "godlike" },

	    // golden pan
	    { "golden pan",   "golden pan" },
	    { "gold pan",     "golden pan" },
	    { "gold and pan", "golden pan" },
	    { "golden pen",   "golden pan" },
	    { "golden fan",   "golden pan" },
	    { "golden plan",  "golden pan" },
	    { "gold in pan",  "golden pan" },

	    // grindr
	    { "grindr",     "grindr" },
	    { "grinder",    "grindr" },
	    { "grind her",  "grindr" },
	    { "grind r",    "grindr" },
	    { "grind",      "grindr" },
	    { "grined her", "grindr" },
	    { "crynder",    "grindr" },

	    // hammer
	    { "hammer",  "hammer" },
	    { "ham her", "hammer" },
	    { "hamer",   "hammer" },
	    { "hummer",  "hammer" },
	    { "stammer", "hammer" },
	    { "grammar", "hammer" },
	    { "armor",   "hammer" },

	    // heartbeats
	    { "heartbeats",  "heartbeats" },
	    { "heart beats", "heartbeats" },
	    { "hard beats",  "heartbeats" },
	    { "heartbeat",   "heartbeats" },
	    { "heart peaks", "heartbeats" },
	    { "hurt beats",  "heartbeats" },
	    { "hard peaks",  "heartbeats" },

	    // hello there
	    { "hello there",   "hello there" },
	    { "hello bear",    "hello there" },
	    { "hell oh there", "hello there" },
	    { "hello their",   "hello there" },
	    { "hello dare",    "hello there" },
	    { "hollow there",  "hello there" },
	    { "yellow there",  "hello there" },

	    // holy shit
	    { "holy shit",   "holy shit" },
	    { "holy sit",    "holy shit" },
	    { "holy shirt",  "holy shit" },
	    { "holly shit",  "holy shit" },
	    { "holy shoot",  "holy shit" },
	    { "wholly shit", "holy shit" },
	    { "holy hit",    "holy shit" },

	    // instant transmission
	    { "instant transmission",  "instant transmission" },
	    { "instinct transmission", "instant transmission" },
	    { "instant transition",    "instant transmission" },
	    { "instant mission",       "instant transmission" },
	    { "in stent transmission", "instant transmission" },
	    { "instance transmission", "instant transmission" },
	    { "instant transmit",      "instant transmission" },

	    // interesting
	    { "interesting",   "interesting" },
	    { "interest",      "interesting" },
	    { "inter esting",  "interesting" },
	    { "inner resting", "interesting" },
	    { "interest ing",  "interesting" },
	    { "inner esting",  "interesting" },
	    { "intresting",    "interesting" },

	    // jeopardy
	    { "jeopardy",       "jeopardy" },
	    { "jeopardee",      "jeopardy" },
	    { "jepardee",       "jeopardy" },
	    { "jeopardy theme", "jeopardy" },
	    { "jeff hardy",     "jeopardy" },
	    { "shepardy",       "jeopardy" },
	    { "lepardy",        "jeopardy" },

	    // knocking
	    { "knocking",       "knocking" },
	    { "nocking",        "knocking" },
	    { "knocking sound", "knocking" },
	    { "knocking door",  "knocking" },
	    { "docking",        "knocking" },
	    { "mocking",        "knocking" },
	    { "knocking on",    "knocking" },

	    // mario jump
	    { "mario jump",    "mario jump" },
	    { "marry oh jump", "mario jump" },
	    { "mario ump",     "mario jump" },
	    { "mario chump",   "mario jump" },
	    { "mario dump",    "mario jump" },
	    { "mario trump",   "mario jump" },

	    // mario power up
	    { "mario power up",    "mario power up" },
	    { "mario power-up",    "mario power up" },
	    { "marry oh power up", "mario power up" },
	    { "mario tower up",    "mario power up" },
	    { "mario fire up",     "mario power up" },
	    { "mario powerup",     "mario power up" },
	    { "mario mower up",    "mario power up" },

	    // nice
	    { "nice",  "nice" },
	    { "noise", "nice" },
	    { "niece", "nice" },
	    { "mice",  "nice" },
	    { "rice",  "nice" },
	    { "noice", "nice" },
	    { "ice",   "nice" },

	    // nope
	    { "nope", "nope" },
	    { "hope", "nope" },
	    { "rope", "nope" },
	    { "node", "nope" },
	    { "no",   "nope" },
	    { "note", "nope" },
	    { "mote", "nope" },

	    // nut
	    { "nut",   "nut" },
	    { "not",   "nut" },
	    { "nuts",  "nut" },
	    { "net",   "nut" },
	    { "butt",  "nut" },
	    { "cut",   "nut" },

	    // oh my god
	    { "oh my god",  "oh my god" },
	    { "omg",        "oh my god" },
	    { "oh my gad",  "oh my god" },
	    { "all my god", "oh my god" },
	    { "oh my gut",  "oh my god" },
	    { "oh my dog",  "oh my god" },

	    // pan
	    { "pan",  "pan" },
	    { "pen",  "pan" },
	    { "pin",  "pan" },
	    { "fan",  "pan" },
	    { "plan", "pan" },
	    { "can",  "pan" },
	    { "tan",  "pan" },

	    // pop
	    { "pop", "pop" },
	    { "bop", "pop" },
	    { "mop", "pop" },
	    { "pod", "pop" },
	    { "pot", "pop" },
	    { "pup", "pop" },

	    // quack
	    { "quack",       "quack" },
	    { "quack sound", "quack" },
	    { "quick",       "quack" },
	    { "ack",         "quack" },
	    { "quacked",     "quack" },
	    { "crack",       "quack" },
	    { "whack",       "quack" },

	    // rizz
	    { "rizz",    "rizz" },
	    { "riz",     "rizz" },
	    { "ris",     "rizz" },
	    { "rich",    "rizz" },
	    { "wrist",   "rizz" },
	    { "ridge",   "rizz" },
	    { "rizz is", "rizz" },

	    // rubber ducky
	    { "rubber ducky",  "rubber ducky" },
	    { "rubber duck",   "rubber ducky" },
	    { "robber ducky",  "rubber ducky" },
	    { "rubber duckie", "rubber ducky" },
	    { "rubber lucky",  "rubber ducky" },
	    { "rub her ducky", "rubber ducky" },
	    { "rubber dusky",  "rubber ducky" },

	    // sad trombone
	    { "sad trombone",       "sad trombone" },
	    { "sad trom bone",      "sad trombone" },
	    { "sad drone bone",     "sad trombone" },
	    { "bad trombone",       "sad trombone" },
	    { "sad trombone sound", "sad trombone" },
	    { "sad trom-bone",      "sad trombone" },
	    { "sad chrome bone",    "sad trombone" },

	    // scored
	    { "scored", "scored" },
	    { "score",  "scored" },
	    { "chord",  "scored" },
	    { "board",  "scored" },
	    { "soured", "scored" },
	    { "scared", "scored" },
	    { "stored", "scored" },

	    // shocking
	    { "shocking", "shocking" },
	    { "shocker",  "shocking" },
	    { "shucking", "shocking" },
	    { "stocking", "shocking" },
	    { "locking",  "shocking" },
	    { "shaking",  "shocking" },
	    { "shoking",  "shocking" },

	    // startle
	    { "startle",       "startle" },
	    { "startle sound", "startle" },
	    { "start el",      "startle" },
	    { "startled",      "startle" },
	    { "stardel",       "startle" },
	    { "tartle",        "startle" },
	    { "star toll",     "startle" },

	    // taco bell
	    { "taco bell",       "taco bell" },
	    { "talk oh bell",    "taco bell" },
	    { "taco bell sound", "taco bell" },
	    { "taco bell bong",  "taco bell" },
	    { "tackle bell",     "taco bell" },
	    { "taco bill",       "taco bell" },
	    { "toggle bell",     "taco bell" },

	    // uwu
	    { "uwu",     "uwu" },
	    { "oo woo",  "uwu" },
	    { "oowoo",   "uwu" },
	    { "you woo", "uwu" },
	    { "u w u",   "uwu" },
	    { "woo woo", "uwu" },

	    // whip
	    { "whip",       "whip" },
	    { "hwip",       "whip" },
	    { "whip sound", "whip" },
	    { "hip",        "whip" },
	    { "trip",       "whip" },
	    { "flip",       "whip" },
	    { "whipped",    "whip" },

	    // wow
	    { "wow",       "wow" },
	    { "how",       "wow" },
	    { "vow",       "wow" },
	    { "now",       "wow" },
	    { "woah",      "wow" },
	    { "whoa",      "wow" },
	    { "wow sound", "wow" },

	    // yay
	    { "yay",       "yay" },
	    { "hey",       "yay" },
	    { "say",       "yay" },
	    { "yay sound", "yay" },
	    
	    #endregion
	};
	private static readonly Dictionary<string, string> s_vocabularyWords     = new()
	{ 
		#region Commands
		
	    { "a",                    "a"                    },
		{ "act",                  "act"                  },
		{ "affec",                "affec"                },
		{ "affect",               "affect"               },
		{ "affects",              "affects"              },
		{ "after",                "after"                },
		{ "afk",                  "afk"                  },
		{ "alright",              "alright"              },
		{ "always",               "always"               },
		{ "amatar",               "amatar"               },
		{ "an",                   "an"                   },
		{ "appatar",              "appatar"              },
		{ "aptar",                "aptar"                },
		{ "aptars",               "aptars"               },
		{ "as",                   "as"                   },
		{ "ask",                  "ask"                  },
		{ "askt",                 "askt"                 },
		{ "asp",                  "asp"                  },
		{ "ast",                  "ast"                  },
		{ "avatar",               "avatar"               },
		{ "avatarish",            "avatarish"            },
		{ "avatars",              "avatars"              },
		{ "avatarsh",             "avatarsh"             },
		{ "avatarz",              "avatarz"              },
		{ "avata",                "avata"                },
		{ "avtar",                "avtar"                },
		{ "avtars",               "avtars"               },
		{ "away",                 "away"                 },
		{ "ax",                   "ax"                   },
		{ "axe",                  "axe"                  },
		{ "b",                    "b"                    },
		{ "bace",                 "bace"                 },
		{ "bach",                 "bach"                 },
		{ "bad",                  "bad"                  },
		{ "badge",                "badge"                },
		{ "badged",               "badged"               },
		{ "bags",                 "bags"                 },
		{ "baise",                "baise"                },
		{ "bass",                 "bass"                 },
		{ "baste",                "baste"                },
		{ "batch",                "batch"                },
		{ "batched",              "batched"              },
		{ "be",                   "be"                   },
		{ "beam",                 "beam"                 },
		{ "bee",                  "bee"                  },
		{ "beast",                "beast"                },
		{ "best",                 "best"                 },
		{ "board",                "board"                },
		{ "bored",                "bored"                },
		{ "brake",                "brake"                },
		{ "break",                "break"                },
		{ "breaked",              "breaked"              },
		{ "brick",                "brick"                },
		{ "brock",                "brock"                },
		{ "check",                "check"                },
		{ "checked",              "checked"              },
		{ "checkt",               "checkt"               },
		{ "cheque",               "cheque"               },
		{ "chick",                "chick"                },
		{ "chord",                "chord"                },
		{ "cord",                 "cord"                 },
		{ "chump",                "chump"                },
		{ "coat",                 "coat"                 },
		{ "code",                 "code"                 },
		{ "coded",                "coded"                },
		{ "coed",                 "coed"                 },
		{ "cold",                 "cold"                 },
		{ "collar",               "collar"               },
		{ "collars",              "collars"              },
		{ "coller",               "coller"               },
		{ "color",                "color"                },
		{ "colors",               "colors"               },
		{ "colour",               "colour"               },
		{ "colours",              "colours"              },
		{ "could",                "could"                },
		{ "cover",                "cover"                },
		{ "covers",               "covers"               },
		{ "cream",                "cream"                },
		{ "culler",               "culler"               },
		{ "cullers",              "cullers"              },
		{ "daunt",                "daunt"                },
		{ "de-fault",             "de-fault"             },
		{ "dee",                  "dee"                  },
		{ "defalt",               "defalt"               },
		{ "default",              "default"              },
		{ "d-fault",              "d-fault"              },
		{ "dice",                 "dice"                 },
		{ "dies",                 "dies"                 },
		{ "dose",                 "dose"                 },
		{ "draw",                 "draw"                 },
		{ "drop",                 "drop"                 },
		{ "dropin",               "dropin"               },
		{ "dropping",             "dropping"             },
		{ "droppin",              "droppin"              },
		{ "dump",                 "dump"                 },
		{ "effec",                "effec"                },
		{ "effective",            "effective"            },
		{ "effect",               "effect"               },
		{ "effects",              "effects"              },
		{ "egg",                  "egg"                  },
		{ "esk",                  "esk"                  },
		{ "ex",                   "ex"                   },
		{ "expload",              "expload"              },
		{ "exploaded",            "exploaded"            },
		{ "explode",              "explode"              },
		{ "eye",                  "eye"                  },
		{ "f",                    "f"                    },
		{ "face",                 "face"                 },
		{ "fact",                 "fact"                 },
		{ "fault",                "fault"                },
		{ "faux",                 "faux"                 },
		{ "focas",                "focas"                },
		{ "focus",                "focus"                },
		{ "fokus",                "fokus"                },
		{ "folks",                "folks"                },
		{ "for",                  "for"                  },
		{ "fortress",             "fortress"             },
		{ "freak",                "freak"                },
		{ "free",                 "free"                 },
		{ "freeview",             "freeview"             },
		{ "from",                 "from"                 },
		{ "front",                "front"                },
		{ "full",                 "full"                 },
		{ "game",                 "game"                 },
		{ "gave",                 "gave"                 },
		{ "gill",                 "gill"                 },
		{ "give",                 "give"                 },
		{ "giveaway",             "giveaway"             },
		{ "gump",                 "gump"                 },
		{ "habatar",              "habatar"              },
		{ "haunt",                "haunt"                },
		{ "hi",                   "hi"                   },
		{ "high",                 "high"                 },
		{ "hill",                 "hill"                 },
		{ "hocus",                "hocus"                },
		{ "hydrate",              "hydrate"              },
		{ "hydrated",             "hydrated"             },
		{ "implode",              "implode"              },
		{ "in",                   "in"                   },
		{ "infect",               "infect"               },
		{ "infects",              "infects"              },
		{ "ink",                  "ink"                  },
		{ "is",                   "is"                   },
		{ "ites",                 "ites"                 },
		{ "jack",                 "jack"                 },
		{ "jump",                 "jump"                 },
		{ "jumped",               "jumped"               },
		{ "junk",                 "junk"                 },
		{ "k",                    "k"                    },
		{ "keel",                 "keel"                 },
		{ "key",                  "key"                  },
		{ "keyboard",             "keyboard"             },
		{ "kill",                 "kill"                 },
		{ "killed",               "killed"               },
		{ "kiss",                 "kiss"                 },
		{ "knee",                 "knee"                 },
		{ "kohl",                 "kohl"                 },
		{ "lane",                 "lane"                 },
		{ "lay",                  "lay"                  },
		{ "layed",                "layed"                },
		{ "layer",                "layer"                },
		{ "layout",               "layout"               },
		{ "light",                "light"                },
		{ "lights",               "lights"               },
		{ "likes",                "likes"                },
		{ "line",                 "line"                 },
		{ "lock",                 "lock"                 },
		{ "locust",               "locust"               },
		{ "long",                 "long"                 },
		{ "look",                 "look"                 },
		{ "low",                  "low"                  },
		{ "luck",                 "luck"                 },
		{ "lying",                "lying"                },
		{ "main",                 "main"                 },
		{ "mame",                 "mame"                 },
		{ "man",                  "man"                  },
		{ "mane",                 "mane"                 },
		{ "me",                   "me"                   },
		{ "mean",                 "mean"                 },
		{ "metal",                "metal"                },
		{ "mic",                  "mic"                  },
		{ "micro",                "micro"                },
		{ "microphone",           "microphone"           },
		{ "mike",                 "mike"                 },
		{ "might",                "might"                },
		{ "mine",                 "mine"                 },
		{ "modal",                "modal"                },
		{ "model",                "model"                },
		{ "modle",                "modle"                },
		{ "mottle",               "mottle"               },
		{ "muddle",               "muddle"               },
		{ "module",               "module"               },
		{ "my",                   "my"                   },
		{ "naim",                 "naim"                 },
		{ "name",                 "name"                 },
		{ "neigh",                "neigh"                },
		{ "neme",                 "neme"                 },
		{ "nice",                 "nice"                 },
		{ "night",                "night"                },
		{ "nights",               "nights"               },
		{ "nitro",                "nitro"                },
		{ "not",                  "not"                  },
		{ "nsfw",                 "nsfw"                 },
		{ "nutshell",             "nutshell"             },
		{ "o",                    "o"                    },
		{ "obs",                  "obs"                  },
		{ "ode",                  "ode"                  },
		{ "of",                   "of"                   },
		{ "oh",                   "oh"                   },
		{ "on",                   "on"                   },
		{ "one",                  "one"                  },
		{ "out",                  "out"                  },
		{ "outline",              "outline"              },
		{ "outlyne",              "outlyne"              },
		{ "ovatar",               "ovatar"               },
		{ "pace",                 "pace"                 },
		{ "page",                 "page"                 },
		{ "paper",                "paper"                },
		{ "papper",               "papper"               },
		{ "pash",                 "pash"                 },
		{ "pay",                  "pay"                  },
		{ "payor",                "payor"                },
		{ "peat",                 "peat"                 },
		{ "per",                  "per"                  },
		{ "pervew",               "pervew"               },
		{ "phase",                "phase"                },
		{ "phone",                "phone"                },
		{ "phocus",               "phocus"               },
		{ "pin",                  "pin"                  },
		{ "piper",                "piper"                },
		{ "pre",                  "pre"                  },
		{ "preview",              "preview"              },
		{ "pre-view",             "pre-view"             },
		{ "privew",               "privew"               },
		{ "purview",              "purview"              },
		{ "rack",                 "rack"                 },
		{ "rake",                 "rake"                 },
		{ "rawk",                 "rawk"                 },
		{ "re",                   "re"                   },
		{ "ream",                 "ream"                 },
		{ "receipt",              "receipt"              },
		{ "recent",               "recent"               },
		{ "record",               "record"               },
		{ "recording",            "recording"            },
		{ "records",              "records"              },
		{ "repeat",               "repeat"               },
		{ "repeating",            "repeating"            },
		{ "repeats",              "repeats"              },
		{ "repel",                "repel"                },
		{ "reprieve",             "reprieve"             },
		{ "reprieves",            "reprieves"            },
		{ "reset",                "reset"                },
		{ "resetting",            "resetting"            },
		{ "rest",                 "rest"                 },
		{ "retreat",              "retreat"              },
		{ "retreats",             "retreat"              },
		{ "retreating",           "retreating"           },
		{ "rights",               "rights"               },
		{ "robin",                "robin"                },
		{ "roc",                  "roc"                  },
		{ "rock",                 "rock"                 },
		{ "role",                 "role"                 },
		{ "roll",                 "roll"                 },
		{ "rtd",                  "rtd"                  },
		{ "s",                    "s"                    },
		{ "safe",                 "safe"                 },
		{ "said",                 "said"                 },
		{ "sang",                 "sang"                 },
		{ "sat",                  "sat"                  },
		{ "save",                 "save"                 },
		{ "say",                  "say"                  },
		{ "scip",                 "scip"                 },
		{ "scis",                 "scis"                 },
		{ "scissors",             "scissors"             },
		{ "scissers",             "scissers"             },
		{ "scream",               "scream"               },
		{ "sect",                 "sect"                 },
		{ "see",                  "see"                  },
		{ "sent",                 "sent"                 },
		{ "ser-vices",            "ser-vices"            },
		{ "service",              "service"              },
		{ "services",             "services"             },
		{ "serviced",             "serviced"             },
		{ "servises",             "servises"             },
		{ "servis",               "servis"               },
		{ "set",                  "set"                  },
		{ "sfx",                  "sfx"                  },
		{ "shack",                "shack"                },
		{ "shader",               "shader"               },
		{ "shader0",              "shader0"              },
		{ "shader1",              "shader1"              },
		{ "shader2",              "shader2"              },
		{ "shader3",              "shader3"              },
		{ "shatter",              "shatter"              },
		{ "ship",                 "ship"                 },
		{ "shop",                 "shop"                 },
		{ "shutter",              "shutter"              },
		{ "sit",                  "sit"                  },
		{ "sisters",              "sisters"              },
		{ "sizors",               "sizors"               },
		{ "sizzers",              "sizzers"              },
		{ "sizzors",              "sizzors"              },
		{ "sketch",               "sketch"               },
		{ "skin",                 "skin"                 },
		{ "skip",                 "skip"                 },
		{ "skips",                "skips"                },
		{ "skit",                 "skit"                 },
		{ "smart",                "smart"                },
		{ "some",                 "some"                 },
		{ "song",                 "song"                 },
		{ "songs",                "songs"                },
		{ "sop",                  "sop"                  },
		{ "sound",                "sound"                },
		{ "splode",               "splode"               },
		{ "stark",                "stark"                },
		{ "start",                "start"                },
		{ "starting",             "starting"             },
		{ "starts",               "starts"               },
		{ "stat",                 "stat"                 },
		{ "steam",                "steam"                },
		{ "step",                 "step"                 },
		{ "stock",                "stock"                },
		{ "stop",                 "stop"                 },
		{ "stops",                "stops"                },
		{ "stopp",                "stopp"                },
		{ "stopping",             "stopping"             },
		{ "stort",                "stort"                },
		{ "stratch",              "stratch"              },
		{ "stream",               "stream"               },
		{ "strech",               "strech"               },
		{ "stretch",              "stretch"              },
		{ "stretsh",              "stretsh"              },
		{ "stritch",              "stritch"              },
		{ "strong",               "strong"               },
		{ "sung",                 "sung"                 },
		{ "surface",              "surface"              },
		{ "surfaces",             "surfaces"             },
		{ "sut",                  "sut"                  },
		{ "t",                    "t"                    },
		{ "tame",                 "tame"                 },
		{ "taper",                "taper"                },
		{ "tart",                 "tart"                 },
		{ "taught",               "taught"               },
		{ "taunt",                "taunt"                },
		{ "taut",                 "taut"                 },
		{ "team",                 "team"                 },
		{ "teen",                 "teen"                 },
		{ "teepee",               "teepee"               },
		{ "tessed",               "tessed"               },
		{ "test",                 "test"                 },
		{ "tf2",                  "tf2"                  },
		{ "the",                  "the"                  },
		{ "thong",                "thong"                },
		{ "three",                "three"                },
		{ "tidal",                "tidal"                },
		{ "tidals",               "tidals"               },
		{ "tidle",                "tidle"                },
		{ "tidles",               "tidles"               },
		{ "tight",                "tight"                },
		{ "tightal",              "tightal"              },
		{ "tightel",              "tightel"              },
		{ "tights",               "tights"               },
		{ "tist",                 "tist"                 },
		{ "tital",                "tital"                },
		{ "titals",               "titals"               },
		{ "tite",                 "tite"                 },
		{ "titels",               "titels"               },
		{ "title",                "title"                },
		{ "titles",               "titles"               },
		{ "to",                   "to"                   },
		{ "tont",                 "tont"                 },
		{ "too",                  "too"                  },
		{ "top",                  "top"                  },
		{ "tost",                 "tost"                 },
		{ "tree",                 "tree"                 },
		{ "tretch",               "tretch"               },
		{ "two",                  "two"                  },
		{ "tyke",                 "tyke"                 },
		{ "ump",                  "ump"                  },
		{ "un-lock",              "un-lock"              },
		{ "unlock",               "unlock"               },
		{ "unlocked",             "unlocked"             },
		{ "unlocking",            "unlocking"            },
		{ "unlocks",              "unlocks"              },
		{ "unluck",               "unluck"               },
		{ "us",                   "us"                   },
		{ "uvatar",               "uvatar"               },
		{ "vapor",                "vapor"                },
		{ "vase",                 "vase"                 },
		{ "view",                 "view"                 },
		{ "wait",                 "wait"                 },
		{ "walk",                 "walk"                 },
		{ "walked",               "walked"               },
		{ "walking",              "walking"              },
		{ "walks",                "walks"                },
		{ "wall",                 "wall"                 },
		{ "war",                  "war"                  },
		{ "ware",                 "ware"                 },
		{ "way",                  "way"                  },
		{ "we",                   "we"                   },
		{ "west",                 "west"                 },
		{ "when",                 "when"                 },
		{ "whites",               "whites"               },
		{ "will",                 "will"                 },
		{ "wok",                  "wok"                  },
		{ "woke",                 "woke"                 },
		{ "won",                  "won"                  },
		{ "work",                 "work"                 },
		{ "worked",               "worked"               },
		{ "workout",              "workout"              },
		{ "zero",                 "zero"                 },
		
		#endregion

	    #region Colors
	    
		{ "ann",           "ann"           },
		{ "and",           "and"           },
		{ "arenge",        "arenge"        },
		{ "oringe",        "oringe"        },
		{ "arrnge",        "arrnge"        },
		{ "banana",        "banana"        },
		{ "berry",         "berry"         },
		{ "blew",          "blew"          },
		{ "bloo",          "bloo"          },
		{ "blue",          "blue"          },
		{ "bonanza",       "bonanza"       },
		{ "boo",           "boo"           },
		{ "bow",           "bow"           },
		{ "brass",         "brass"         },
		{ "bunk",          "bunk"          },
		{ "burple",        "burple"        },
		{ "clean",         "clean"         },
		{ "clue",          "clue"          },
		{ "creen",         "creen"         },
		{ "circle",        "circle"        },
		{ "creamsicle",    "creamsicle"    },
		{ "cyan",          "cyan"          },
		{ "cyann",         "cyann"         },
		{ "cyber",         "cyber"         },
		{ "cyberpunk",     "cyberpunk"     },
		{ "cycle",         "cycle"         },
		{ "cypher",        "cypher"        },
		{ "dock",          "dock"          },
		{ "drag",          "drag"          },
		{ "dragonfruit",   "dragonfruit"   },
		{ "dragon",        "dragon"        },
		{ "eat",           "eat"           },
		{ "favour",        "favour"        },
		{ "fed",           "fed"           },
		{ "feet",          "feet"          },
		{ "fellow",        "fellow"        },
		{ "forced",        "forced"        },
		{ "forest",        "forest"        },
		{ "forrest",       "forrest"       },
		{ "fruit",         "fruit"         },
		{ "genta",         "genta"         },
		{ "gentle",        "gentle"        },
		{ "glue",          "glue"          },
		{ "go",            "go"            },
		{ "grean",         "grean"         },
		{ "green",         "green"         },
		{ "grin",          "grin"          },
		{ "head",          "head"          },
		{ "heat",          "heat"          },
		{ "heatwave",      "heatwave"      },
		{ "heatweave",     "heatweave"     },
		{ "heet",          "heet"          },
		{ "height",        "height"        },
		{ "hellow",        "hellow"        },
		{ "i",             "i"             },
		{ "ice",           "ice"           },
		{ "icey",          "icey"          },
		{ "icy",           "icy"           },
		{ "ight",          "ight"          },
		{ "keen",          "keen"          },
		{ "lime",          "lime"          },
		{ "ma",            "ma"            },
		{ "magent",        "magent"        },
		{ "magenta",       "magenta"       },
		{ "mailing",       "mailing"       },
		{ "majenta",       "majenta"       },
		{ "melon",         "melon"         },
		{ "mellen",        "mellen"        },
		{ "mellow",        "mellow"        },
		{ "million",       "million"       },
		{ "monk",          "monk"          },
		{ "muh",           "muh"           },
		{ "nana",          "nana"          },
		{ "orange",        "orange"        },
		{ "ornge",         "ornge"         },
		{ "or",            "or"            },
		{ "people",        "people"        },
		{ "perple",        "perple"        },
		{ "punk",          "punk"          },
		{ "purp",          "purp"          },
		{ "purpel",        "purpel"        },
		{ "purple",        "purple"        },
		{ "quite",         "quite"         },
		{ "rad",           "rad"           },
		{ "rainbow",       "rainbow"       },
		{ "rainbeau",      "rainbeau"      },
		{ "rain",          "rain"          },
		{ "rane",          "rane"          },
		{ "range",         "range"         },
		{ "rasberry",      "rasberry"      },
		{ "raspberry",     "raspberry"     },
		{ "raz",           "raz"           },
		{ "razz",          "razz"          },
		{ "read",          "read"          },
		{ "red",           "red"           },
		{ "reign",         "reign"         },
		{ "rhyme",         "rhyme"         },
		{ "rid",           "rid"           },
		{ "sabre",         "sabre"         },
		{ "science",       "science"       },
		{ "scion",         "scion"         },
		{ "sea",           "sea"           },
		{ "si",            "si"            },
		{ "sick",          "sick"          },
		{ "sickle",        "sickle"        },
		{ "sigh",          "sigh"          },
		{ "sipped",        "sipped"        },
		{ "son",           "son"           },
		{ "son set",       "son set"       },
		{ "straw",         "straw"         },
		{ "strawberry",    "strawberry"    },
		{ "sun",           "sun"           },
		{ "sunset",        "sunset"        },
		{ "sunsit",        "sunsit"        },
		{ "sy",            "sy"            },
		{ "talk",          "talk"          },
		{ "tocks",         "tocks"         },
		{ "tock",          "tock"          },
		{ "toxic",         "toxic"         },
		{ "toxick",        "toxick"        },
		{ "toxit",         "toxit"         },
		{ "urple",         "urple"         },
		{ "vaper",         "vaper"         },
		{ "vaperwave",     "vaperwave"     },
		{ "vape",          "vape"          },
		{ "vaporwave",     "vaporwave"     },
		{ "water",         "water"         },
		{ "watermelon",    "watermelon"    },
		{ "watt",          "watt"          },
		{ "watter",        "watter"        },
		{ "wave",          "wave"          },
		{ "wayne",         "wayne"         },
		{ "white",         "white"         },
		{ "whyte",         "whyte"         },
		{ "wight",         "wight"         },
		{ "yallow",        "yallow"        },
		{ "yell",          "yell"          },
		{ "yello",         "yello"         },
		{ "yellow",        "yellow"        },
		
		#endregion
	    
	    #region Effects
	    
		{ "acts",                 "acts"                 },
		{ "alectric",             "alectric"             },
		{ "armer",                "armer"                },
		{ "armor",                "armor"                },
		{ "armour",               "armour"               },
		{ "articles",             "articles"             },
		{ "arts",                 "arts"                 },
		{ "atomic",               "atomic"               },
		{ "audits",               "audits"               },
		{ "balling",              "balling"              },
		{ "barks",                "barks"                },
		{ "bars",                 "bars"                 },
		{ "binary",               "binary"               },
		{ "binery",               "binery"               },
		{ "bindery",              "bindery"              },
		{ "bi-nary",              "bi-nary"              },
		{ "bio",                  "bio"                  },
		{ "bi-oh",                "bi-oh"                },
		{ "biosparks",            "biosparks"            },
		{ "bitstream",            "bitstream"            },
		{ "bit-stream",           "bit-stream"           },
		{ "blow",                 "blow"                 },
		{ "boobs",                "boobs"                },
		{ "boretex",              "boretex"              },
		{ "bread",                "bread"                },
		{ "bring",                "bring"                },
		{ "bubbles",              "bubbles"              },
		{ "bubs",                 "bubs"                 },
		{ "buy",                  "buy"                  },
		{ "calls",                "calls"                },
		{ "cards",                "cards"                },
		{ "cars",                 "cars"                 },
		{ "cellular",             "cellular"             },
		{ "cheeks",               "cheeks"               },
		{ "circuit",              "circuit"              },
		{ "circulate",            "circulate"            },
		{ "circut",               "circut"               },
		{ "clash",                "clash"                },
		{ "clockwise",            "clockwise"            },
		{ "comb",                 "comb"                 },
		{ "cracking",             "cracking"             },
		{ "cracks",               "cracks"               },
		{ "crax",                 "crax"                 },
		{ "cubes",                "cubes"                },
		{ "dadda",                "dadda"                },
		{ "dark",                 "dark"                 },
		{ "darks",                "darks"                },
		{ "data",                 "data"                 },
		{ "day",                  "day"                  },
		{ "dayta",                "dayta"                },
		{ "diagonal",             "diagonal"             },
		{ "diagnal",              "diagnal"              },
		{ "diamond",              "diamond"              },
		{ "die",                  "die"                  },
		{ "dimon",                "dimon"                },
		{ "dimond",               "dimond"               },
		{ "ditch",                "ditch"                },
		{ "doom",                 "doom"                 },
		{ "dot",                  "dot"                  },
		{ "doubles",              "doubles"              },
		{ "draft",                "draft"                },
		{ "dreads",               "dreads"               },
		{ "drift",                "drift"                },
		{ "dye",                  "dye"                  },
		{ "electric",             "electric"             },
		{ "e-lectric",            "e-lectric"            },
		{ "energy",               "energy"               },
		{ "en-ergy",              "en-ergy"              },
		{ "extreme",              "extreme"              },
		{ "falling",              "falling"              },
		{ "fear",                 "fear"                 },
		{ "field",                "field"                },
		{ "filing",               "filing"               },
		{ "finds",                "finds"                },
		{ "finery",               "finery"               },
		{ "fitstream",            "fitstream"            },
		{ "flash",                "flash"                },
		{ "flesh",                "flesh"                },
		{ "flitch",               "flitch"               },
		{ "floe",                 "floe"                 },
		{ "flow",                 "flow"                 },
		{ "fortex",               "fortex"               },
		{ "frac",                 "frac"                 },
		{ "fractal",              "fractal"              },
		{ "fractel",              "fractel"              },
		{ "fractor",              "fractor"              },
		{ "fragile",              "fragile"              },
		{ "garfield",             "garfield"             },
		{ "gift",                 "gift"                 },
		{ "glich",                "glich"                },
		{ "glitch",               "glitch"               },
		{ "glock",                "glock"                },
		{ "greed",                "greed"                },
		{ "grid",                 "grid"                 },
		{ "gripes",               "gripes"               },
		{ "grit",                 "grit"                 },
		{ "heads",                "heads"                },
		{ "healed",               "healed"               },
		{ "heck",                 "heck"                 },
		{ "hex",                  "hex"                  },
		{ "honey",                "honey"                },
		{ "honeycomb",            "honeycomb"            },
		{ "inner",                "inner"                },
		{ "inter-ference",        "inter-ference"        },
		{ "interference",         "interference"         },
		{ "iso",                  "iso"                  },
		{ "it",                   "it"                   },
		{ "king",                 "king"                 },
		{ "knights",              "knights"              },
		{ "la",                   "la"                   },
		{ "larva",                "larva"                },
		{ "lash",                 "lash"                 },
		{ "laser",                "laser"                },
		{ "lava",                 "lava"                 },
		{ "lazer",                "lazer"                },
		{ "lazy",                 "lazy"                 },
		{ "lectric",              "lectric"              },
		{ "lift",                 "lift"                 },
		{ "lines",                "lines"                },
		{ "lyces",                "lyces"                },
		{ "magma",                "magma"                },
		{ "magna",                "magna"                },
		{ "mama",                 "mama"                 },
		{ "manites",              "manites"              },
		{ "mat",                  "mat"                  },
		{ "matrics",              "matrics"              },
		{ "matrix",               "matrix"               },
		{ "mattress",             "mattress"             },
		{ "may",                  "may"                  },
		{ "midnight",             "midnight"             },
		{ "midstream",            "midstream"            },
		{ "moire",                "moire"                },
		{ "money",                "money"                },
		{ "mooving",              "mooving"              },
		{ "more",                 "more"                 },
		{ "moving",               "moving"               },
		{ "moy",                  "moy"                  },
		{ "mwah",                 "mwah"                 },
		{ "mwahr",                "mwahr"                },
		{ "nan",                  "nan"                  },
		{ "nanites",              "nanites"              },
		{ "nannys",               "nannys"               },
		{ "n-ergy",               "n-ergy"               },
		{ "neural",               "neural"               },
		{ "neuro",                "neuro"                },
		{ "new",                  "new"                  },
		{ "nipples",              "nipples"              },
		{ "nites",                "nites"                },
		{ "nural",                "nural"                },
		{ "objects",              "objects"              },
		{ "orbits",               "orbits"               },
		{ "orders",               "orders"               },
		{ "outward",              "outward"              },
		{ "over-drive",           "over-drive"           },
		{ "overdrive",            "overdrive"            },
		{ "overly",               "overly"               },
		{ "parks",                "parks"                },
		{ "part",                 "part"                 },
		{ "particals",            "particals"            },
		{ "particles",            "particles"            },
		{ "pause",                "pause"                },
		{ "peaks",                "peaks"                },
		{ "pilling",              "pilling"              },
		{ "pings",                "pings"                },
		{ "pinks",                "pinks"                },
		{ "plasma",               "plasma"               },
		{ "plazma",               "plazma"               },
		{ "pole",                 "pole"                 },
		{ "potticles",            "potticles"            },
		{ "power",                "power"                },
		{ "prices",               "prices"               },
		{ "pubes",                "pubes"                },
		{ "puddles",              "puddles"              },
		{ "pulls",                "pulls"                },
		{ "pulse",                "pulse"                },
		{ "puls",                 "puls"                 },
		{ "radicals",             "radicals"             },
		{ "radar",                "radar"                },
		{ "raider",               "raider"               },
		{ "racks",                "racks"                },
		{ "rel",                  "rel"                  },
		{ "rgb",                  "rgb"                  },
		{ "riddles",              "riddles"              },
		{ "rift",                 "rift"                 },
		{ "ring",                 "ring"                 },
		{ "rippels",              "rippels"              },
		{ "ripples",              "ripples"              },
		{ "rolling",              "rolling"              },
		{ "room",                 "room"                 },
		{ "rotating",             "rotating"             },
		{ "row",                  "row"                  },
		{ "rowing",               "rowing"               },
		{ "scan",                 "scan"                 },
		{ "scam",                 "scam"                 },
		{ "scrolling",            "scrolling"            },
		{ "sebra",                "sebra"                },
		{ "search",               "search"               },
		{ "seeks",                "seeks"                },
		{ "sell",                 "sell"                 },
		{ "settler",              "settler"              },
		{ "shards",               "shards"               },
		{ "sharks",               "sharks"               },
		{ "sheeled",              "sheeled"              },
		{ "shreds",               "shreds"               },
		{ "shield",               "shield"               },
		{ "shift",                "shift"                },
		{ "shocks",               "shocks"               },
		{ "shots",                "shots"                },
		{ "signs",                "signs"                },
		{ "silk",                 "silk"                 },
		{ "slices",               "slices"               },
		{ "sleep",                "sleep"                },
		{ "soak",                 "soak"                 },
		{ "solar",                "solar"                },
		{ "sonar",                "sonar"                },
		{ "sooner",               "sooner"               },
		{ "span",                 "span"                 },
		{ "sparks",               "sparks"               },
		{ "spinning",             "spinning"             },
		{ "square",               "square"               },
		{ "starfield",            "starfield"            },
		{ "static",               "static"               },
		{ "stars",                "stars"                },
		{ "stellar",              "stellar"              },
		{ "streets",              "streets"              },
		{ "streaks",              "streaks"              },
		{ "stripes",              "stripes"              },
		{ "styling",              "styling"              },
		{ "suite",                "suite"                },
		{ "surkit",               "surkit"               },
		{ "swarming",             "swarming"             },
		{ "sweep",                "sweep"                },
		{ "sweet",                "sweet"                },
		{ "tal",                  "tal"                  },
		{ "taupe",                "taupe"                },
		{ "tiling",               "tiling"               },
		{ "tit",                  "tit"                  },
		{ "toe",                  "toe"                  },
		{ "tonnel",               "tonnel"               },
		{ "topo",                 "topo"                 },
		{ "topographical",        "topographical"        },
		{ "total",                "total"                },
		{ "tower",                "tower"                },
		{ "tracks",               "tracks"               },
		{ "tracking",             "tracking"             },
		{ "traching",             "traching"             },
		{ "tranking",             "tranking"             },
		{ "treads",               "treads"               },
		{ "threads",              "threads"              },
		{ "triples",              "triples"              },
		{ "tripes",               "tripes"               },
		{ "trix",                 "trix"                 },
		{ "tubes",                "tubes"                },
		{ "tunnel",               "tunnel"               },
		{ "types",                "types"                },
		{ "ular",                 "ular"                 },
		{ "va",                   "va"                   },
		{ "vertex",               "vertex"               },
		{ "vertical",             "vertical"             },
		{ "vines",                "vines"                },
		{ "vortex",               "vortex"               },
		{ "vurtical",             "vurtical"             },
		{ "wartex",               "wartex"               },
		{ "weep",                 "weep"                 },
		{ "weigh",                "weigh"                },
		{ "wind",                 "wind"                 },
		{ "wing",                 "wing"                 },
		{ "wipes",                "wipes"                },
		{ "witch",                "witch"                },
		{ "x",                    "x"                    },
		{ "yards",                "yards"                },
		{ "zebra",                "zebra"                },
		{ "zer",                  "zer"                  },
		{ "zibra",                "zibra"                },
		{ "zoom",                 "zoom"                 },
		
		#endregion
	    
		#region Emotes
		
		{ "brate",                "brate"                },
		{ "bumper",               "bumper"               },
		{ "car",                  "car"                  },
		{ "card",                 "card"                 },
		{ "celebrate",            "celebrate"            },
		{ "celebrating",          "celebrating"          },
		{ "celibate",             "celibate"             },
		{ "cell",                 "cell"                 },
		{ "conga",                "conga"                },
		{ "congo",                "congo"                },
		{ "core",                 "core"                 },
		{ "fit",                  "fit"                  },
		{ "gonga",                "gonga"                },
		{ "half",                 "half"                 },
		{ "khonga",               "khonga"               },
		{ "konga",                "konga"                },
		{ "laf",                  "laf"                  },
		{ "laugh",                "laugh"                },
		{ "laughing",             "laughing"             },
		{ "lit",                  "lit"                  },
		{ "longer",               "longer"               },
		{ "number",               "number"               },
		{ "off",                  "off"                  },
		{ "phantom",              "phantom"              },
		{ "pumper",               "pumper"               },
		{ "ran",                  "ran"                  },
		{ "randam",               "randam"               },
		{ "random",               "random"               },
		{ "randomly",             "randomly"             },
		{ "randum",               "randum"               },
		{ "rps",                  "rps"                  },
		{ "seller",               "seller"               },
		{ "shit",                 "shit"                 },
		{ "sister",               "sister"               },
		{ "special",              "special"              },
		{ "special1",             "special1"             },
		{ "special2",             "special2"             },
		{ "specialist",           "specialist"           },
		{ "speshul",              "speshul"              },
		{ "staff",                "staff"                },
		{ "them",                 "them"                 },
		
		#endregion
		
		#region Models
		
		{ "acid",                "acid"                },
		{ "aeroplane",           "aeroplane"           },
		{ "ag",                  "ag"                  },
		{ "air",                 "air"                 },
		{ "airplane",            "airplane"            },
		{ "anned",               "anned"               },
		{ "art",                 "art"                 },
		{ "assteroid",           "assteroid"           },
		{ "aster-oid",           "aster-oid"           },
		{ "asteroid",            "asteroid"            },
		{ "asteroyd",            "asteroyd"            },
		{ "astra",               "astra"               },
		{ "ay",                  "ay"                  },
		{ "bananna",             "bananna"             },
		{ "band",                "band"                },
		{ "bank",                "bank"                },
		{ "beers",               "beers"               },
		{ "blast",               "blast"               },
		{ "boan",                "boan"                },
		{ "bomb",                "bomb"                },
		{ "bone",                "bone"                },
		{ "boned",               "boned"               },
		{ "boobies",             "boobies"             },
		{ "boos",                "boos"                },
		{ "boots",               "boots"               },
		{ "born",                "born"                },
		{ "brain",               "brain"               },
		{ "braine",              "braine"              },
		{ "branch",              "branch"              },
		{ "brancht",             "brancht"             },
		{ "brance",              "brance"              },
		{ "brane",               "brane"               },
		{ "bransh",              "bransh"              },
		{ "brantch",             "brantch"             },
		{ "bray",                "bray"                },
		{ "bred",                "bred"                },
		{ "brook",               "brook"               },
		{ "bubble",              "bubble"              },
		{ "buh",                 "buh"                 },
		{ "bye",                 "bye"                 },
		{ "centree",             "centree"             },
		{ "century",             "century"             },
		{ "cloud",               "cloud"               },
		{ "clowd",               "clowd"               },
		{ "clowed",              "clowed"              },
		{ "clown",               "clown"               },
		{ "coast",               "coast"               },
		{ "companion",           "companion"           },
		{ "compansion",          "compansion"          },
		{ "company",             "company"             },
		{ "companyan",           "companyan"           },
		{ "cone",                "cone"                },
		{ "cotana",              "cotana"              },
		{ "cube",                "cube"                },
		{ "cumin",               "cumin"               },
		{ "cute",                "cute"                },
		{ "dank",                "dank"                },
		{ "deep",                "deep"                },
		{ "died",                "died"                },
		{ "dina",                "dina"                },
		{ "dino",                "dino"                },
		{ "dinosaur",            "dinosaur"            },
		{ "dish",                "dish"                },
		{ "do",                  "do"                  },
		{ "doe",                 "doe"                 },
		{ "done",                "done"                },
		{ "donut",               "donut"               },
		{ "double",              "double"              },
		{ "dough",               "dough"               },
		{ "doughnut",            "doughnut"            },
		{ "drain",               "drain"               },
		{ "duck",                "duck"                },
		{ "dug",                 "dug"                 },
		{ "dugtreeo",            "dugtreeo"            },
		{ "dugtrio",             "dugtrio"             },
		{ "dynasore",            "dynasore"            },
		{ "dynosore",            "dynosore"            },
		{ "edge",                "edge"                },
		{ "eh",                  "eh"                  },
		{ "err",                 "err"                 },
		{ "euphoria",            "euphoria"            },
		{ "fan",                 "fan"                 },
		{ "fears",               "fears"               },
		{ "felix",               "felix"               },
		{ "fighter",             "fighter"             },
		{ "fish",                "fish"                },
		{ "flack",               "flack"               },
		{ "flas",                "flas"                },
		{ "flask",               "flask"               },
		{ "flast",               "flast"               },
		{ "flying",              "flying"              },
		{ "fry",                 "fry"                 },
		{ "frying",              "frying"              },
		{ "fun",                 "fun"                 },
		{ "gears",               "gears"               },
		{ "geers",               "geers"               },
		{ "ghost",               "ghost"               },
		{ "glad",                "glad"                },
		{ "gladdis",             "gladdis"             },
		{ "gladis",              "gladis"              },
		{ "glados",              "glados"              },
		{ "gon",                 "gon"                 },
		{ "gone",                "gone"                },
		{ "grain",               "grain"               },
		{ "gun",                 "gun"                 },
		{ "hair",                "hair"                },
		{ "hand",                "hand"                },
		{ "hanned",              "hanned"              },
		{ "hard",                "hard"                },
		{ "hart",                "hart"                },
		{ "hat",                 "hat"                 },
		{ "hatsune",             "hatsune"             },
		{ "heal",                "heal"                },
		{ "healix",              "healix"              },
		{ "heart",               "heart"               },
		{ "helix",               "helix"               },
		{ "hert",                "hert"                },
		{ "host",                "host"                },
		{ "hot",                 "hot"                 },
		{ "hu",                  "hu"                  },
		{ "hue",                 "hue"                 },
		{ "hume",                "hume"                },
		{ "human",               "human"               },
		{ "id",                  "id"                  },
		{ "jelly",               "jelly"               },
		{ "jellyfish",           "jellyfish"           },
		{ "ka",                  "ka"                  },
		{ "ka-tana",             "ka-tana"             },
		{ "katana",              "katana"              },
		{ "ket",                 "ket"                 },
		{ "kitana",              "kitana"              },
		{ "land",                "land"                },
		{ "licks",               "licks"               },
		{ "loud",                "loud"                },
		{ "mash",                "mash"                },
		{ "miku",                "miku"                },
		{ "much",                "much"                },
		{ "mush",                "mush"                },
		{ "mushroom",            "mushroom"            },
		{ "must",                "must"                },
		{ "no",                  "no"                  },
		{ "ockto",               "ockto"               },
		{ "octapus",             "octapus"             },
		{ "octo",                "octo"                },
		{ "octopus",             "octopus"             },
		{ "os",                  "os"                  },
		{ "own",                 "own"                 },
		{ "pan",                 "pan"                 },
		{ "panama",              "panama"              },
		{ "peen",                "peen"                },
		{ "peenus",              "peenus"              },
		{ "pen",                 "pen"                 },
		{ "penis",               "penis"               },
		{ "pennice",             "pennice"             },
		{ "pennys",              "pennys"              },
		{ "pick",                "pick"                },
		{ "pikman",              "pikman"              },
		{ "pikmin",              "pikmin"              },
		{ "plain",               "plain"               },
		{ "plane",               "plane"               },
		{ "po",                  "po"                  },
		{ "poca",                "poca"                },
		{ "pokeball",            "pokeball"            },
		{ "pokey",               "pokey"               },
		{ "post",                "post"                },
		{ "potato",              "potato"              },
		{ "potahto",             "potahto"             },
		{ "potayto",             "potayto"             },
		{ "proud",               "proud"               },
		{ "pus",                 "pus"                 },
		{ "puss",                "puss"                },
		{ "q",                   "q"                   },
		{ "quid",                "quid"                },
		{ "ranch",               "ranch"               },
		{ "raw",                 "raw"                 },
		{ "robot",               "robot"               },
		{ "rocket",              "rocket"              },
		{ "roebot",              "roebot"              },
		{ "rook",                "rook"                },
		{ "root",                "root"                },
		{ "rouke",               "rouke"               },
		{ "sank",                "sank"                },
		{ "saur",                "saur"                },
		{ "sen",                 "sen"                 },
		{ "sentree",             "sentree"             },
		{ "sentry",              "sentry"              },
		{ "skid",                "skid"                },
		{ "skwed",               "skwed"               },
		{ "skwid",               "skwid"               },
		{ "slider",              "slider"              },
		{ "sno",                 "sno"                 },
		{ "snow",                "snow"                },
		{ "snowman",             "snowman"             },
		{ "soler",               "soler"               },
		{ "sore",                "sore"                },
		{ "soul",               "soul"                 },
		{ "spider",              "spider"              },
		{ "spy",                 "spy"                 },
		{ "spyder",              "spyder"              },
		{ "squad",               "squad"               },
		{ "squid",               "squid"               },
		{ "stair",               "stair"               },
		{ "star",                "star"                },
		{ "stare",               "stare"               },
		{ "starr",               "starr"               },
		{ "sticky",              "sticky"              },
		{ "store",               "store"               },
		{ "system",              "system"              },
		{ "tank",                "tank"                },
		{ "tana",                "tana"                },
		{ "tato",                "tato"                },
		{ "thai",                "thai"                },
		{ "thank",               "thank"               },
		{ "thee",                "thee"                },
		{ "tie",                 "tie"                 },
		{ "trio",                "trio"                },
		{ "tread",               "tread"               },
		{ "tri",                 "tri"                 },
		{ "triangle",            "triangle"            },
		{ "try",                 "try"                 },
		{ "tube",                "tube"                },
		{ "tye",                 "tye"                 },
		{ "ufo",                 "ufo"                 },
		{ "wider",               "wider"               },
		{ "wish",                "wish"                },
		{ "xwing",               "xwing"               },
		{ "years",               "years"               },
		{ "you",                 "you"                 },
		
		#endregion
		
		#region Sound Effects
		
		{ "ack",                  "ack"                  },
		{ "airborn",              "airborn"              },
		{ "airhorn",              "airhorn"              },
		{ "all",                  "all"                  },
		{ "ap-plause",            "ap-plause"            },
		{ "apple",                "apple"                },
		{ "applaws",              "applaws"              },
		{ "applause",             "applause"             },
		{ "ash",                  "ash"                  },
		{ "ass",                  "ass"                  },
		{ "ball",                 "ball"                 },
		{ "balls",                "balls"                },
		{ "ballz",                "ballz"                },
		{ "bart",                 "bart"                 },
		{ "bill",                 "bill"                 },
		{ "bit",                  "bit"                  },
		{ "blank",                "blank"                },
		{ "blink",                "blink"                },
		{ "blinked",              "blinked"              },
		{ "blinking",             "blinking"             },
		{ "boeing",               "boeing"               },
		{ "boing",                "boing"                },
		{ "boink",                "boink"                },
		{ "bong",                 "bong"                 },
		{ "bonk",                 "bonk"                 },
		{ "bonked",               "bonked"               },
		{ "bop",                  "bop"                  },
		{ "boring",               "boring"               },
		{ "bowls",                "bowls"                },
		{ "brah",                 "brah"                 },
		{ "brew",                 "brew"                 },
		{ "bro",                  "bro"                  },
		{ "brush",                "brush"                },
		{ "bruh",                 "bruh"                 },
		{ "busier",               "busier"               },
		{ "butt",                 "butt"                 },
		{ "butter",               "butter"               },
		{ "buzz",                 "buzz"                 },
		{ "buzzer",               "buzzer"               },
		{ "can",                  "can"                  },
		{ "censor",               "censor"               },
		{ "censure",              "censure"              },
		{ "center",               "center"               },
		{ "char",                 "char"                 },
		{ "chrome",               "chrome"               },
		{ "clapping",             "clapping"             },
		{ "clink",                "clink"                },
		{ "crack",                "crack"                },
		{ "crit",                 "crit"                 },
		{ "critical",             "critical"             },
		{ "critically",           "critically"           },
		{ "crynder",              "crynder"              },
		{ "cut",                  "cut"                  },
		{ "dart",                 "dart"                 },
		{ "discord",              "discord"              },
		{ "dis-cord",             "dis-cord"             },
		{ "dischord",             "dischord"             },
		{ "discored",             "discored"             },
		{ "disk",                 "disk"                 },
		{ "docking",              "docking"              },
		{ "doing",                "doing"                },
		{ "drone",                "drone"                },
		{ "dumb",                 "dumb"                 },
		{ "dun",                  "dun"                  },
		{ "dun-dun",              "dun-dun"              },
		{ "ear",                  "ear"                  },
		{ "est",                  "est"                  },
		{ "esting",               "esting"               },
		{ "falls",                "falls"                },
		{ "fart",                 "fart"                 },
		{ "farted",               "farted"               },
		{ "fire",                 "fire"                 },
		{ "flip",                 "flip"                 },
		{ "fort",                 "fort"                 },
		{ "fuzzer",               "fuzzer"               },
		{ "gad",                  "gad"                  },
		{ "god",                  "god"                  },
		{ "godlike",              "godlike"              },
		{ "godly",                "godly"                },
		{ "going",                "going"                },
		{ "gold",                 "gold"                 },
		{ "golden",               "golden"               },
		{ "grammar",              "grammar"              },
		{ "grind",                "grind"                },
		{ "grinder",              "grinder"              },
		{ "grindr",               "grindr"               },
		{ "guard",                "guard"                },
		{ "gut",                  "gut"                  },
		{ "ham",                  "ham"                  },
		{ "hamer",                "hamer"                },
		{ "hammer",               "hammer"               },
		{ "has",                  "has"                  },
		{ "heartbeats",           "heartbeats"           },
		{ "hell",                 "hell"                 },
		{ "hello",                "hello"                },
		{ "hey",                  "hey"                  },
		{ "hip",                  "hip"                  },
		{ "hit",                  "hit"                  },
		{ "hollow",               "hollow"               },
		{ "holly",                "holly"                },
		{ "holy",                 "holy"                 },
		{ "honk",                 "honk"                 },
		{ "hope",                 "hope"                 },
		{ "how",                  "how"                  },
		{ "hummer",               "hummer"               },
		{ "hurt",                 "hurt"                 },
		{ "hwip",                 "hwip"                 },
		{ "instinct",             "instinct"             },
		{ "instant",              "instant"              },
		{ "interest",             "interest"             },
		{ "interesting",          "interesting"          },
		{ "intresting",           "intresting"           },
		{ "jeopardee",            "jeopardee"            },
		{ "jeopardy",             "jeopardy"             },
		{ "jepardee",             "jepardee"             },
		{ "knocking",             "knocking"             },
		{ "laws",                 "laws"                 },
		{ "lepardy",              "lepardy"              },
		{ "link",                 "link"                 },
		{ "locking",              "locking"              },
		{ "marry",                "marry"                },
		{ "mice",                 "mice"                 },
		{ "mission",              "mission"              },
		{ "mocking",              "mocking"              },
		{ "mop",                  "mop"                  },
		{ "mote",                 "mote"                 },
		{ "mower",                "mower"                },
		{ "mussier",              "mussier"              },
		{ "net",                  "net"                  },
		{ "niece",                "niece"                },
		{ "nocking",              "nocking"              },
		{ "node",                 "node"                 },
		{ "noice",                "noice"                },
		{ "noise",                "noise"                },
		{ "nope",                 "nope"                 },
		{ "note",                 "note"                 },
		{ "now",                  "now"                  },
		{ "nut",                  "nut"                  },
		{ "nuts",                 "nuts"                 },
		{ "omg",                  "omg"                  },
		{ "oo",                   "oo"                   },
		{ "oowoo",                "oowoo"                },
		{ "plan",                 "plan"                 },
		{ "plink",                "plink"                },
		{ "pod",                  "pod"                  },
		{ "political",            "political"            },
		{ "pop",                  "pop"                  },
		{ "pot",                  "pot"                  },
		{ "power-up",             "power-up"             },
		{ "powerup",              "powerup"              },
		{ "pup",                  "pup"                  },
		{ "quack",                "quack"                },
		{ "quacked",              "quacked"              },
		{ "quick",                "quick"                },
		{ "r",                    "r"                    },
		{ "rice",                 "rice"                 },
		{ "rich",                 "rich"                 },
		{ "ridge",                "ridge"                },
		{ "ris",                  "ris"                  },
		{ "riz",                  "riz"                  },
		{ "rizz",                 "rizz"                 },
		{ "robber",               "robber"               },
		{ "rope",                 "rope"                 },
		{ "rough",                "rough"                },
		{ "rubber",               "rubber"               },
		{ "sad",                  "sad"                  },
		{ "scared",               "scared"               },
		{ "score",                "score"                },
		{ "scored",               "scored"               },
		{ "sender",               "sender"               },
		{ "senser",               "senser"               },
		{ "sensor",               "sensor"               },
		{ "shaking",              "shaking"              },
		{ "shepardy",             "shepardy"             },
		{ "shirt",                "shirt"                },
		{ "shocking",             "shocking"             },
		{ "shocker",              "shocker"              },
		{ "shoking",              "shoking"              },
		{ "shoot",                "shoot"                },
		{ "shucking",             "shucking"             },
		{ "sincere",              "sincere"              },
		{ "soured",               "soured"               },
		{ "stammer",              "stammer"              },
		{ "stardel",              "stardel"              },
		{ "startle",              "startle"              },
		{ "startled",             "startled"             },
		{ "stent",                "stent"                },
		{ "stocking",             "stocking"             },
		{ "stored",               "stored"               },
		{ "tackle",               "tackle"               },
		{ "taco",                 "taco"                 },
		{ "tan",                  "tan"                  },
		{ "tartle",               "tartle"               },
		{ "their",                "their"                },
		{ "theme",                "theme"                },
		{ "this",                 "this"                 },
		{ "thorn",                "thorn"                },
		{ "toggle",               "toggle"               },
		{ "ton",                  "ton"                  },
		{ "transition",           "transition"           },
		{ "transmission",         "transmission"         },
		{ "transmit",             "transmit"             },
		{ "trip",                 "trip"                 },
		{ "trombone",             "trombone"             },
		{ "trump",                "trump"                },
		{ "u",                    "u"                    },
		{ "up",                   "up"                   },
		{ "uwu",                  "uwu"                  },
		{ "vow",                  "vow"                  },
		{ "w",                    "w"                    },
		{ "walls",                "walls"                },
		{ "whack",                "whack"                },
		{ "whip",                 "whip"                 },
		{ "whipped",              "whipped"              },
		{ "whoa",                 "whoa"                 },
		{ "wholly",               "wholly"               },
		{ "woah",                 "woah"                 },
		{ "woo",                  "woo"                  },
		{ "wow",                  "wow"                  },
		{ "wrist",                "wrist"                },
		{ "yay",                  "yay"                  },
		
		#endregion
	};

	[GeneratedRegex(
		pattern:     @"\b(dagger|agger|bagger|beggar|bigger|dag|digger|daggered|tagger|stagger|daggers|dag her|dad ger|deck her|dagg er|tagger|tiger|a dagger)\b",
		options:     RegexOptions.IgnoreCase, 
		cultureName: "en-US"
	)]
	private static partial Regex CommandRegex();
	
	[GeneratedRegex(
		pattern: @"text='(.*?)'"
	)]
	private static partial Regex VoiceRecognitionRegex();

	private bool                   m_shutdown    = false;
	private readonly PacketPeerUdp m_udpPeer     = new();
	private string                 m_voiceBuffer = string.Empty;

	private static string GetStaticTokenMapping(
		string token
	)
	{
		return VoiceController.s_vocabularyPhrases.TryGetValue(
			key:   token, 
			value: out var mappedValue
		) ? mappedValue : string.Empty;
	}
	
	private static void HandleVoiceMessage(
		string message
	)
	{
		var text   = VoiceController.NormalizeText(
			text: message
		);
		var tokens = text.Split(
			separator: ' ',
			options:   StringSplitOptions.RemoveEmptyEntries
		);
		
		if (tokens.Length is 0)
		{
			return;
		}
		
		var startIndex = -1;
		for (var i = 0; i < tokens.Length; i++)
		{
			if (
				VoiceController.s_nameMap.ContainsKey(
					key: tokens[i]
				) is true
			)
			{
				startIndex = i;
				break;
			}
		}

		if (
			startIndex is -1 ||
			startIndex >= tokens.Length - 1
		)
		{
			return;
		}

		var validTokens = VoiceController.ParseTokens(
			tokens: tokens[(startIndex + 1)..]
		);

		if (validTokens.Count > 0)
		{
			string command;
			
			var trigger = validTokens[index: 0];
			switch (trigger)
			{
				case "lights" when validTokens.Count >= 3:
					command = $"!lights {validTokens[1]}, {string.Join(" ", validTokens[2..])}";
					break;
				
				case "avatar" when validTokens.Count >= 3 && validTokens[1] is "set":
				{
					var target = validTokens[2];

					if (
						target.StartsWith(
							value: "shader"
						) is true && 
						validTokens.Count >= 5
					)
					{
						var effect = validTokens[3];
						var color  = string.Join(
							separator: ' ', 
							values:    validTokens[4..]
						);
						command = $"!avatar set {target} {effect}, {color}";
					}
					else
					{
						command = $"!{string.Join(" ", validTokens)}";
					}

					break;
				}
				
				default:
					command = $"!{string.Join(" ", validTokens)}";
					break;
			}
			
			var payloadMessage = new ServiceJoystickWebSocketPayloadMessage
			{
				Author =
				{
					Username     = VoiceController.c_username,
					IsModerator  = false,
					IsStreamer   = true,
					IsSubscriber = false
				},
				Text   = command,
			};
			
			ServiceJoystickWebSocketPayloadChatHandler.HandleBotCommands(
				payloadMessage: payloadMessage
			);
		}
	}
	
	private static string NormalizeText(
		string text
	)
	{
		var textNormalized = text.ToLower();
		var stringBuilder  = new StringBuilder();
		foreach (
			var character in textNormalized.Where(
				character => 
					char.IsLetterOrDigit(
					 	 c: character
				  	) is true ||
				  	char.IsWhiteSpace(
					 	 c: character
				  	) is true
				)
			)
		{
			stringBuilder.Append(
				value: character
			);
		}
		
		var result = stringBuilder.ToString();
		
		var commandRegex = VoiceController.CommandRegex();
		result = commandRegex.Replace(
			input:       result, 
			replacement: $"dagger"
		);

		result = VoiceController.s_numberMap.Aggregate(
			seed: result, 
			func: (current, pair) => 
				Replace(
					input:       current, 
					pattern:     $@"\b{pair.Key}\b", 
					replacement: pair.Value
				)
		);

		result = result.Replace(
			oldValue: $"shader ",
			newValue: $"shader"
		);

		return result;
	}

	private static List<string> ParseTokens(
		string[] tokens
	)
	{
		var validated = new List<string>();

		for (var i = 0; i < tokens.Length; i++)
		{
			var token = tokens[i];
			
			if (
				VoiceController.s_openTokens.TryGetValue(
					key:   token, 
					value: out var value
				) is true
			)
			{
				validated.Add(
					item: value
				);

				var nextIndex = i + 1;
				if (nextIndex < tokens.Length)
				{
					var payload = string.Join(
						separator: ' ',
						tokens[nextIndex..]
					);
					validated.Add(
						item: payload
					);
				}
				
				return validated;
			}

			if (
				VoiceController.s_vocabularyWords.ContainsKey(
					key: token
				) is false
			)
			{
				continue;
			}

			var mapping   = string.Empty;
			var lookAhead = 0;
			
			for (var len = 5; len >= 1; len--)
			{
				var index = i + (len - 1);
				if (index < tokens.Length)
				{
					var nextToken = tokens[index];
					if (
						VoiceController.s_vocabularyWords.ContainsKey(
							key: nextToken
						) is false
					)
					{
						continue;
					}
					
					var combined = string.Join(
						separator:  ' ',
						value:      tokens, 
						startIndex: i, 
						count:      len
					);
					mapping = VoiceController.GetStaticTokenMapping(
						token: combined
					);
					if (mapping != string.Empty)
					{
						lookAhead = len - 1;
						break;
					}
				}
			}

			if (mapping != string.Empty)
			{
				validated.Add(
					item: mapping
				);
				i += lookAhead;
			}
		}

		return validated;
	}
	
	private static void SendDelayedBotMessage(
		string message
	)
	{
		Task.Run(
			function: async () =>
			{
				await Task.Delay(
					millisecondsDelay: 200
				);
                
				var serviceJoystickBot = Services.Services.GetService<ServiceJoystickBot>();
				serviceJoystickBot.SendChatMessageSilently(
					message: message
				);
			}
		);
	}

	private void BindPeer()
    {
    	_ = this.m_udpPeer.Bind(
    		port: VoiceController.c_port
    	);
    }
	
	private void StartProcessingPeer()
	{
		Task.Run(
			function: async () =>
			{
				while (this.m_shutdown is false)
				{
					await Task.Delay(
						millisecondsDelay: VoiceController.c_delayInMilliseconds
					);
					
					while (this.m_udpPeer.GetAvailablePacketCount() > 0)
					{
						var packet = this.m_udpPeer.GetPacket();
						if (packet.Length > 0)
						{
							var payload = Encoding.UTF8.GetString(
								bytes: packet
							);
				
							var regex = VoiceController.VoiceRecognitionRegex();
							var match = regex.Match(
								input: payload
							);
							if (match.Success is true)
							{
								var message = match.Groups[1].Value;
								if (
									string.IsNullOrEmpty(
										value: message
									) is true
								)
								{
									if (
										string.IsNullOrWhiteSpace(
											value: this.m_voiceBuffer
										) is false
									)
									{
										VoiceController.HandleVoiceMessage(
											message: this.m_voiceBuffer
										);
										this.m_voiceBuffer = string.Empty;
									}
									continue;
								}
					
								this.m_voiceBuffer = $"{this.m_voiceBuffer} {message}".Trim();
							}
						}
					}
				}
			}
		);
	}

	private void StopProcessingPeer()
	{
		this.m_shutdown = true;
	}
}