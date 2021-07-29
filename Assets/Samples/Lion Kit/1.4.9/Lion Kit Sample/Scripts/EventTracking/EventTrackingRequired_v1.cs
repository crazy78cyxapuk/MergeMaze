using UnityEngine;
using Random = UnityEngine.Random;
using LionStudios;

public class EventTrackingRequired_v1 : EventTrackingRequiredBase
{
	public void TrackLevelStarted()
	{
		Analytics.Events.LevelStarted(Random.Range(1, 11), "No Rank");
	}

	public void TrackLevelRestart()
	{
		Analytics.Events.LevelRestart(Random.Range(1, 11), "No Rank");
	}

	public void TrackLevelSkip()
	{
		Analytics.Events.LevelSkipped(Random.Range(1, 11), "No Rank");
	}

	public void TrackLevelFailed()
	{
		Analytics.Events.LevelFailed(Random.Range(1, 11), Random.Range(1, 500));
	}

	public void TrackLevelComplete()
	{
		Analytics.Events.LevelComplete(Random.Range(1, 11), Random.Range(1, 500));
	}

	public void TrackUpgradePurchase()
	{
		Analytics.Events.UpgradePurchase("RandomItem", Random.Range(1, 11), GetTransaction());
	}

	public void TrackTutorialComplete()
	{
		Analytics.Events.TutorialComplete();
	}

	public void TrackHighScoreUnlocked()
	{
		Analytics.Events.HighScoreUnlocked(Random.Range(1, 100), Random.Range(1, 500));
	}

	public void TrackRoundStarted()
	{
		Analytics.Events.RoundStarted(Random.Range(1, 50));
	}

	public void TrackContentUnlocked()
	{
		Analytics.Events.ContentUnlocked("UnlockedContentExampleName", 88);
	}

	public void TrackRewardVideoStarted()
	{
		Analytics.Events.RewardVideoStart("RVPlacementName", 3);
	}

	public void TrackRewardVideoEnded()
	{
		Analytics.Events.RewardVideoEnd("RVPlacementName", 4);
	}

	public void TrackRoundComplete()
	{
		string roundResult = "";
		switch (Random.Range(0, 3))
		{
			case 0: roundResult = "Win"; break;
			case 1: roundResult = "Loss"; break;
			case 2: roundResult = "Undecided"; break;
		}
		Analytics.Events.RoundComplete(roundResult);
	}

	//public void TrackLevelAttempt()
	//{
	//	Analytics.Events.LevelAttempt(Random.Range(1, 111), true, Random.Range(0f, 999999f), Random.Range(1, 9999), "Test Group A", GetTransaction());
	//}

	//public void TrackSessions()
	//{
	//    int[] sessionCounts = { 1, 10, 25, 50, 100 };
	//    int numSessions = sessionCounts[Random.Range(0, sessionCounts.Length)];
	//    Analytics.Events.TrackSessions(numSessions, Random.Range(1, 11), GetRandomTestGroup(), GetCurrencies());
	//}

	//public void TrackLevelAttempt()
	//{
	//    Analytics.Events.LevelAttempt(Random.Range(1, 11), Random.Range(0, 2) == 1, Random.Range(1, 1000), Random.Range(1, 11), GetRandomTestGroup(), GetTransaction());
	//}

	//public void TrackTutorialAttempt()
	//{
	//    Analytics.Events.TutorialAttempt("tutorial_" + Random.Range(1, 11), Random.Range(0, 2) == 1, Random.Range(1, 11), GetRandomTestGroup(), GetTransaction());
	//}

	//public void TrackItemUpgrade()
	//{
	//    string[] randomContent = { "axe", "staff", "book", "wolf_pelt", "sword" };
	//    string content = randomContent[Random.Range(0, randomContent.Length)];
	//    Analytics.Events.ItemUpgrade(content, Random.Range(1, 100), GetRandomTestGroup(), GetTransactions());
	//}

	//public void TrackCharacterUpgrade()
	//{
	//    string[] randomContent = { "shelia", "bob", "boris", "delila", "axelrod" };
	//    string content = randomContent[Random.Range(0, randomContent.Length)];
	//    Analytics.Events.ItemUpgrade(content, Random.Range(1, 100), GetRandomTestGroup(), GetTransactions());
	//}

	//public void TrackBuildingUpgrade()
	//{
	//    string[] randomContent = { "tower", "farm", "keep", "post_office", "night_club" };
	//    string content = randomContent[Random.Range(0, randomContent.Length)];
	//    Analytics.Events.BuildingUpgrade(content, Random.Range(1, 100), GetRandomTestGroup(), GetTransactions());
	//}

	//public void TrackInAppPurchase()
	//{
	//    string[] randomContent = { "100_gems", "500_gems", "1000_gems", "5000_gems", "10000_gems" };
	//    string content = randomContent[Random.Range(0, randomContent.Length)];
	//    Analytics.Events.InAppPurchase(content, "USD", Random.Range(1f, 100f), Random.Range(1, 100), Random.Range(0, int.MaxValue), GetCurrencies());
	//}

	//public void TrackCurrencyUsage()
	//{
	//    string[] randomContent = { "upgrade", "axe", "powerup", "something", "something_else" };
	//    string content = randomContent[Random.Range(0, randomContent.Length)];
	//    Analytics.Events.CurrencyUsage(content, Random.Range(1, 100), GetTransaction(), GetCurrencies());
	//}

	//public void TrackHintUsage()
	//{
	//    string[] randomContent = { "upgrade", "axe", "powerup", "something", "something_else" };
	//    string content = randomContent[Random.Range(0, randomContent.Length)];
	//    Analytics.Events.HintUsage(Random.Range(1, 100), Random.Range(1, 1000), Random.Range(1,11), GetTransaction());
	//}
}
