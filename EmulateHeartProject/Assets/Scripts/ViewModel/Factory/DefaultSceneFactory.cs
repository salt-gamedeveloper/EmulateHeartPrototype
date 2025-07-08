using System;

public class DefaultSceneFactory : ISceneFactory
{
    public ISceneViewModel CreateViewModel(SceneType sceneType)
    {
        switch (sceneType)
        {/*
            case SceneType.Title:
                return new TitleViewModel(true,false);

            case SceneType.Settings:
                return new SettingsViewModel(
                    new SettingsData(
                        0.2f,
                        0.5f,
                        0.05f,
                        false,
                        GeminiModel.Gemini20Flash,
                        "url",
                        "ApiKey"
                    ));

            case SceneType.Load:
                var dummySummaries = new List<SaveSummaryViewModel>
                {
                    new SaveSummaryViewModel(
                        "Auto Save",
                        "�^�J�V\n�~�h��\n8\n2025/05/05\n15:00"
                    ),
                    new SaveSummaryViewModel(
                        "Slot 1",
                        "���E�R\n�J�Y�I\n2\n2025/05/05\n14:45"
                    ),
                    new SaveSummaryViewModel(
                        "Slot 2",
                        "�P���W\n�q���~\n1\n2025/05/04\n12:30"
                    ),
                    new SaveSummaryViewModel(
                        "Slot 3",
                        "�~�z\n���N\n12\n2025/05/03\n16:20"
                    ),
                    new SaveSummaryViewModel(
                        "Slot 4",
                        "�A���J\n�q�T�V\n5\n2025/05/02\n10:15"
                    )
                };
                return new SaveSummariesViewModel(dummySummaries);
            case SceneType.Extra:
                return new ExtraViewModel();
            */
            default:
                throw new ArgumentException($"�s���ȃV�[���^�C�v: {sceneType}");
        }
    }
}
