
using Npgsql;
using System;
using System.ComponentModel.DataAnnotations;

namespace Overlay.Core.Services.Databases.Models;

internal sealed class ServiceDatabaseAchievementUser() :
    ServiceDatabaseModel()
{
    // Meta Data
    [Key]
    internal string   AchievementUser_Username                                   { get; set; } = string.Empty;
    internal DateTime AchievementUser_First_View_Date                            { get; set; } = DateTime.UtcNow;
    internal DateTime AchievementUser_Last_View_Date                             { get; set; } = DateTime.UtcNow;
    
    // Tips
    internal int      AchievementUser_Buy_Me_A_Banana_Shakes                     { get; set; } = 0;
    internal int      AchievementUser_Buy_Me_A_Dinners                           { get; set; } = 0;
    internal int      AchievementUser_Buy_Me_A_Powerade_Slushies                 { get; set; } = 0;
    internal int      AchievementUser_Colors_Unlocked                            { get; set; } = 0;
    internal int      AchievementUser_Compliments                                { get; set; } = 0;
    internal int      AchievementUser_Dropins                                    { get; set; } = 0;
    internal int      AchievementUser_Effects_Unlocked                           { get; set; } = 0;
    internal int      AchievementUser_Exercises                                  { get; set; } = 0;
    internal int      AchievementUser_Faps                                       { get; set; } = 0;
    internal int      AchievementUser_Firsts                                     { get; set; } = 0;
    internal int      AchievementUser_Gifted_Subs                                { get; set; } = 0;
    internal int      AchievementUser_Gush_Control_Link_Minutes_Used             { get; set; } = 0;
    internal int      AchievementUser_Hellos                                     { get; set; } = 0;
    internal int      AchievementUser_Hydrates                                   { get; set; } = 0;
    internal int      AchievementUser_Interactions                               { get; set; } = 0;
    internal int      AchievementUser_Lights_Changed                             { get; set; } = 0;
    internal int      AchievementUser_Lurks                                      { get; set; } = 0;
    internal int      AchievementUser_Models_Unlocked                            { get; set; } = 0;
    internal int      AchievementUser_Nsfw_Claims                                { get; set; } = 0;
    internal int      AchievementUser_Pay_My_Bills                               { get; set; } = 0;
    internal int      AchievementUser_Sfx_Used                                   { get; set; } = 0;
    internal int      AchievementUser_Showin_Some_Loves                          { get; set; } = 0;
    internal int      AchievementUser_Songs_Requested                            { get; set; } = 0;
    internal int      AchievementUser_Songs_Skipped                              { get; set; } = 0;
    internal int      AchievementUser_Subs                                       { get; set; } = 0;
    internal int      AchievementUser_Tf2_Explodes                               { get; set; } = 0;
    internal int      AchievementUser_Tf2_Kills                                  { get; set; } = 0;
    internal int      AchievementUser_Thanks                                     { get; set; } = 0;
    internal int      AchievementUser_Toke_Ups                                   { get; set; } = 0;
    internal int      AchievementUser_Total_Tips                                 { get; set; } = 0;

    // Meta Achievements
    internal int      AchievementUser_Checked_Banks                              { get; set; } = 0;
    internal int      AchievementUser_Checked_Unlocks                            { get; set; } = 0;
    internal int      AchievementUser_Cleared_Names                              { get; set; } = 0;
    internal int      AchievementUser_Cleared_Titles                             { get; set; } = 0;
    internal int      AchievementUser_Customized_Avatars                         { get; set; } = 0;
    internal int      AchievementUser_Customized_Badges                          { get; set; } = 0;
    internal int      AchievementUser_Customized_Names                           { get; set; } = 0;
    internal int      AchievementUser_Customized_Titles                          { get; set; } = 0;
    internal int      AchievementUser_Linked_Steams                              { get; set; } = 0;
    internal int      AchievementUser_Messages_Sent                              { get; set; } = 0;
    internal int      AchievementUser_Minutes_Watched                            { get; set; } = 0;
    internal int      AchievementUser_Previewed_Unlocks                          { get; set; } = 0;
    internal int      AchievementUser_Questions_Asked                            { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Lose_3_In_A_Rows        { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Losses                  { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Matches_Played          { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Papers                  { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Rocks                   { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Scissors                { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Tie_3_In_A_Rows         { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Ties                    { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Win_3_In_A_Rows         { get; set; } = 0;
    internal int      AchievementUser_Rock_Paper_Scissor_Wins                    { get; set; } = 0;
    internal int      AchievementUser_Rolled_1                                   { get; set; } = 0;
    internal int      AchievementUser_Rolled_42                                  { get; set; } = 0;
    internal int      AchievementUser_Rolled_67                                  { get; set; } = 0;
    internal int      AchievementUser_Rolled_69                                  { get; set; } = 0;
    internal int      AchievementUser_Rolled_100                                 { get; set; } = 0;
    internal int      AchievementUser_Rolls                                      { get; set; } = 0;
    internal int      AchievementUser_Rolls_Maximum                              { get; set; } = 0;
    internal int      AchievementUser_Rolls_Minimum                              { get; set; } = 0;
    internal int      AchievementUser_Tf2_Completed_Bhop_Bonuses                 { get; set; } = 0;
    internal int      AchievementUser_Tf2_Completed_Bhop_Maps                    { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Demomans                        { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Engineers                       { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Heavies                         { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Medics                          { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Pyros                           { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Scouts                          { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Snipers                         { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Soldiers                        { get; set; } = 0;
    internal int      AchievementUser_Tf2_Played_Spies                           { get; set; } = 0;
    internal int      AchievementUser_Tf2_SmoothDagger_Deaths                    { get; set; } = 0;
    internal int      AchievementUser_Tf2_SmoothDagger_Dominated_Bys             { get; set; } = 0;
    internal int      AchievementUser_Tf2_SmoothDagger_Dominations               { get; set; } = 0;
    internal int      AchievementUser_Tf2_SmoothDagger_Kills                     { get; set; } = 0;
    internal int      AchievementUser_Tf2_SmoothDagger_Revenges                  { get; set; } = 0;
    internal int      AchievementUser_Times_Viewed                               { get; set; } = 0;
    
    // Non-Meta Achievements
    internal int      AchievementUser_Rolled_240                                 { get; set; } = 0;
    internal int      AchievementUser_Rolled_256                                 { get; set; } = 0;
    internal int      AchievementUser_Rolled_420                                 { get; set; } = 0;
    internal int      AchievementUser_Rolled_720                                 { get; set; } = 0;
    internal int      AchievementUser_Rolled_1080                                { get; set; } = 0;
    internal int      AchievementUser_Rolled_1337                                { get; set; } = 0;
    internal int      AchievementUser_Rolled_3840                                { get; set; } = 0;
    internal int      AchievementUser_Rolled_100000                              { get; set; } = 0;
    internal int      AchievementUser_Titles_Unlocked                            { get; set; } = 0;
    internal int      AchievementUser_Tracks_Completed                           { get; set; } = 0;

    internal override void CreateFromNpgsqlDataReader(NpgsqlDataReader npgsqlDataReader)
    {
        // Meta Data
        var readerUsername                                  = (string)   npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Username                            )}"];
        var readerFirstViewDate                             = (DateTime) npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_First_View_Date                     )}"];
        var readerLastViewDate                              = (DateTime) npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Last_View_Date                      )}"];
        var readerDropins                                   = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Dropins                             )}"];
        
        // Tips
        var readerBuyMeABananaShakes                        = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Buy_Me_A_Banana_Shakes              )}"];
        var readerBuyMeADinners                             = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Buy_Me_A_Dinners                    )}"];
        var readerBuyMeAPoweradeSlushies                    = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Buy_Me_A_Powerade_Slushies          )}"];
        var readerColorsUnlocked                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Colors_Unlocked                     )}"];
        var readerCompliments                               = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Compliments                         )}"];
        var readerEffectsUnlocked                           = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Effects_Unlocked                    )}"];
        var readerExercises                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Exercises                           )}"];
        var readerFaps                                      = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Faps                                )}"];
        var readerFirsts                                    = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Firsts                              )}"];
        var readerGiftedSubs                                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Gifted_Subs                         )}"];
        var readerGushControlLinkMinutesUsed                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Gush_Control_Link_Minutes_Used      )}"];
        var readerHellos                                    = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Hellos                              )}"];
        var readerHydrates                                  = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Hydrates                            )}"];
        var readerInteractions                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Interactions                        )}"];
        var readerLightsChanged                             = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Lights_Changed                      )}"];
        var readerLurks                                     = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Lurks                               )}"];
        var readerModelsUnlocked                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Models_Unlocked                     )}"];
        var readerNsfwClaims                                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Nsfw_Claims                         )}"];
        var readerPayMyBills                                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Pay_My_Bills                        )}"];
        var readerSfxUsed                                   = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Sfx_Used                            )}"];
        var readerShowinSomeLoves                           = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Showin_Some_Loves                   )}"];
        var readerSongsRequested                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Songs_Requested                     )}"];
        var readerSongsSkipped                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Songs_Skipped                       )}"];
        var readerSubs                                      = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Subs                                )}"];
        var readerTf2Explodes                               = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Explodes                        )}"];
        var readerTf2Kills                                  = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Kills                           )}"];
        var readerThanks                                    = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Thanks                              )}"];
        var readerTokeUps                                   = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Toke_Ups                            )}"];
        var readerTotalTips                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Total_Tips                          )}"];
        
        // Meta Achievements
        var readerCheckedBanks                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Checked_Banks                       )}"];
        var readerCheckedUnlocks                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Checked_Unlocks                     )}"];
        var readerClearedNames                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Cleared_Names                       )}"];
        var readerClearedTitles                             = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Cleared_Titles                      )}"];
        var readerCustomizedAvatars                         = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Customized_Avatars                  )}"];
        var readerCustomizedBadges                          = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Customized_Badges                   )}"];
        var readerCustomizedNames                           = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Customized_Names                    )}"];
        var readerCustomizedTitles                          = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Customized_Titles                   )}"];
        var readerLinkedSteams                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Linked_Steams                       )}"];
        var readerMessagesSent                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Messages_Sent                       )}"];
        var readerMinutesWatched                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Minutes_Watched                     )}"];
        var readerPreviewedUnlocks                          = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Previewed_Unlocks                   )}"];
        var readerQuestionsAsked                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Questions_Asked                     )}"];
        var readerRockPaperScissorLose3InARows              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Lose_3_In_A_Rows )}"];
        var readerRockPaperScissorLosses                    = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Losses           )}"];
        var readerRockPaperScissorMatchesPlayed             = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Matches_Played   )}"];
        var readerRockPaperScissorPapers                    = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Papers           )}"];
        var readerRockPaperScissorRocks                     = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Rocks            )}"];
        var readerRockPaperScissorScissors                  = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Scissors         )}"];
        var readerRockPaperScissorTie3InARows               = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Tie_3_In_A_Rows  )}"];
        var readerRockPaperScissorTies                      = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Ties             )}"];
        var readerRockPaperScissorWin3InARows               = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Win_3_In_A_Rows  )}"];
        var readerRockPaperScissorWins                      = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rock_Paper_Scissor_Wins             )}"];
        var readerRolled1                                   = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_1                            )}"];
        var readerRolled42                                  = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_42                           )}"];
        var readerRolled67                                  = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_67                           )}"];
        var readerRolled69                                  = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_69                           )}"];
        var readerRolled100                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_100                          )}"];
        var readerRolls                                     = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolls                               )}"];
        var readerRollsMaximum                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolls_Maximum                       )}"];
        var readerRollsMinimum                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolls_Minimum                       )}"];
        var readerTf2CompletedBhopBonuses                   = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Completed_Bhop_Bonuses          )}"];
        var readerTf2SmoothDaggerDeaths                     = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Deaths             )}"];
        var readerTf2PlayedDemomans                         = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Demomans                 )}"];
        var readerTf2PlayedEngineers                        = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Engineers                )}"];
        var readerTf2PlayedHeavies                          = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Heavies                  )}"];
        var readerTf2PlayedMedics                           = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Medics                   )}"];
        var readerTf2PlayedPyros                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Pyros                    )}"];
        var readerTf2PlayedScouts                           = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Scouts                   )}"];
        var readerTf2PlayedSnipers                          = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Snipers                  )}"];
        var readerTf2PlayedSoldiers                         = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Soldiers                 )}"];
        var readerTf2PlayedSpies                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Played_Spies                    )}"];
        var readerTf2CompletedBhopMaps                      = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_Completed_Bhop_Maps             )}"];
        var readerTf2SmoothDaggerDominatedBys               = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Dominated_Bys      )}"];
        var readerTf2SmoothDaggerDominations                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Dominations        )}"];
        var readerTf2SmoothDaggerKills                      = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Kills              )}"];
        var readerTf2SmoothDaggerRevenges                   = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tf2_SmoothDagger_Revenges           )}"];
        var readerTimesViewed                               = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Times_Viewed                        )}"];
        
        // Non-Meta Achievements
        var readerRolled240                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_240                          )}"];
        var readerRolled256                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_256                          )}"];
        var readerRolled420                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_420                          )}"];
        var readerRolled720                                 = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_720                          )}"];
        var readerRolled1080                                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_1080                         )}"];
        var readerRolled3840                                = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_3840                         )}"];
        var readerRolled100000                              = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Rolled_100000                       )}"];
        var readerTitlesUnlocked                            = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Titles_Unlocked                     )}"];
        var readerTracksCompleted                           = (int)      npgsqlDataReader[name: $"{nameof( ServiceDatabaseAchievementUser.AchievementUser_Tracks_Completed                    )}"];
        
        // Meta Data
        this.AchievementUser_Username                            = readerUsername;
        this.AchievementUser_First_View_Date                     = readerFirstViewDate;
        this.AchievementUser_Last_View_Date                      = readerLastViewDate;
        this.AchievementUser_Dropins                             = readerDropins;

        // Tips
        this.AchievementUser_Buy_Me_A_Banana_Shakes              = readerBuyMeABananaShakes;
        this.AchievementUser_Buy_Me_A_Dinners                    = readerBuyMeADinners;
        this.AchievementUser_Buy_Me_A_Powerade_Slushies          = readerBuyMeAPoweradeSlushies;
        this.AchievementUser_Colors_Unlocked                     = readerColorsUnlocked;
        this.AchievementUser_Compliments                         = readerCompliments;
        this.AchievementUser_Effects_Unlocked                    = readerEffectsUnlocked;
        this.AchievementUser_Exercises                           = readerExercises;
        this.AchievementUser_Faps                                = readerFaps;
        this.AchievementUser_Firsts                              = readerFirsts;
        this.AchievementUser_Gifted_Subs                         = readerGiftedSubs;
        this.AchievementUser_Gush_Control_Link_Minutes_Used      = readerGushControlLinkMinutesUsed;
        this.AchievementUser_Hellos                              = readerHellos;
        this.AchievementUser_Hydrates                            = readerHydrates;
        this.AchievementUser_Interactions                        = readerInteractions;
        this.AchievementUser_Lights_Changed                      = readerLightsChanged;
        this.AchievementUser_Lurks                               = readerLurks;
        this.AchievementUser_Models_Unlocked                     = readerModelsUnlocked;
        this.AchievementUser_Nsfw_Claims                         = readerNsfwClaims;
        this.AchievementUser_Pay_My_Bills                        = readerPayMyBills;
        this.AchievementUser_Sfx_Used                            = readerSfxUsed;
        this.AchievementUser_Showin_Some_Loves                   = readerShowinSomeLoves;
        this.AchievementUser_Songs_Requested                     = readerSongsRequested;
        this.AchievementUser_Songs_Skipped                       = readerSongsSkipped;
        this.AchievementUser_Subs                                = readerSubs;
        this.AchievementUser_Tf2_Explodes                        = readerTf2Explodes;
        this.AchievementUser_Tf2_Kills                           = readerTf2Kills;
        this.AchievementUser_Thanks                              = readerThanks;
        this.AchievementUser_Toke_Ups                            = readerTokeUps;
        this.AchievementUser_Total_Tips                          = readerTotalTips;
        
        // Meta Achievements
        this.AchievementUser_Checked_Banks                       = readerCheckedBanks;
        this.AchievementUser_Checked_Unlocks                     = readerCheckedUnlocks;
        this.AchievementUser_Cleared_Names                       = readerClearedNames;
        this.AchievementUser_Cleared_Titles                      = readerClearedTitles;
        this.AchievementUser_Customized_Avatars                  = readerCustomizedAvatars;
        this.AchievementUser_Customized_Badges                   = readerCustomizedBadges;
        this.AchievementUser_Customized_Names                    = readerCustomizedNames;
        this.AchievementUser_Customized_Titles                   = readerCustomizedTitles;
        this.AchievementUser_Linked_Steams                       = readerLinkedSteams;
        this.AchievementUser_Messages_Sent                       = readerMessagesSent;
        this.AchievementUser_Minutes_Watched                     = readerMinutesWatched;
        this.AchievementUser_Previewed_Unlocks                   = readerPreviewedUnlocks;
        this.AchievementUser_Questions_Asked                     = readerQuestionsAsked;
        this.AchievementUser_Rock_Paper_Scissor_Lose_3_In_A_Rows = readerRockPaperScissorLose3InARows;
        this.AchievementUser_Rock_Paper_Scissor_Losses           = readerRockPaperScissorLosses;
        this.AchievementUser_Rock_Paper_Scissor_Matches_Played   = readerRockPaperScissorMatchesPlayed;
        this.AchievementUser_Rock_Paper_Scissor_Papers           = readerRockPaperScissorPapers;
        this.AchievementUser_Rock_Paper_Scissor_Rocks            = readerRockPaperScissorRocks;
        this.AchievementUser_Rock_Paper_Scissor_Scissors         = readerRockPaperScissorScissors;
        this.AchievementUser_Rock_Paper_Scissor_Tie_3_In_A_Rows  = readerRockPaperScissorTie3InARows;
        this.AchievementUser_Rock_Paper_Scissor_Ties             = readerRockPaperScissorTies;
        this.AchievementUser_Rock_Paper_Scissor_Win_3_In_A_Rows  = readerRockPaperScissorWin3InARows;
        this.AchievementUser_Rock_Paper_Scissor_Wins             = readerRockPaperScissorWins;
        this.AchievementUser_Rolled_1                            = readerRolled1;
        this.AchievementUser_Rolled_42                           = readerRolled42;
        this.AchievementUser_Rolled_67                           = readerRolled67;
        this.AchievementUser_Rolled_69                           = readerRolled69;
        this.AchievementUser_Rolled_100                          = readerRolled100;
        this.AchievementUser_Rolls                               = readerRolls;
        this.AchievementUser_Rolls_Maximum                       = readerRollsMaximum;
        this.AchievementUser_Rolls_Minimum                       = readerRollsMinimum;
        this.AchievementUser_Tf2_Completed_Bhop_Bonuses          = readerTf2CompletedBhopBonuses;
        this.AchievementUser_Tf2_Completed_Bhop_Maps             = readerTf2CompletedBhopMaps;
        this.AchievementUser_Tf2_Played_Demomans                 = readerTf2PlayedDemomans;
        this.AchievementUser_Tf2_Played_Engineers                = readerTf2PlayedEngineers;
        this.AchievementUser_Tf2_Played_Heavies                  = readerTf2PlayedHeavies;
        this.AchievementUser_Tf2_Played_Medics                   = readerTf2PlayedMedics;
        this.AchievementUser_Tf2_Played_Pyros                    = readerTf2PlayedPyros;
        this.AchievementUser_Tf2_Played_Scouts                   = readerTf2PlayedScouts;
        this.AchievementUser_Tf2_Played_Snipers                  = readerTf2PlayedSnipers;
        this.AchievementUser_Tf2_Played_Soldiers                 = readerTf2PlayedSoldiers;
        this.AchievementUser_Tf2_Played_Spies                    = readerTf2PlayedSpies;
        this.AchievementUser_Tf2_SmoothDagger_Deaths             = readerTf2SmoothDaggerDeaths;
        this.AchievementUser_Tf2_SmoothDagger_Dominated_Bys      = readerTf2SmoothDaggerDominatedBys;
        this.AchievementUser_Tf2_SmoothDagger_Dominations        = readerTf2SmoothDaggerDominations;
        this.AchievementUser_Tf2_SmoothDagger_Kills              = readerTf2SmoothDaggerKills;
        this.AchievementUser_Tf2_SmoothDagger_Revenges           = readerTf2SmoothDaggerRevenges;
        this.AchievementUser_Times_Viewed                        = readerTimesViewed;
        
        // Non-Meta Achievements
        this.AchievementUser_Rolled_240                          = readerRolled240;
        this.AchievementUser_Rolled_256                          = readerRolled256;
        this.AchievementUser_Rolled_420                          = readerRolled420;
        this.AchievementUser_Rolled_720                          = readerRolled720;
        this.AchievementUser_Rolled_1080                         = readerRolled1080;
        this.AchievementUser_Rolled_3840                         = readerRolled3840;
        this.AchievementUser_Rolled_100000                       = readerRolled100000;
        this.AchievementUser_Titles_Unlocked                     = readerTitlesUnlocked;
        this.AchievementUser_Tracks_Completed                    = readerTracksCompleted;
    }
}