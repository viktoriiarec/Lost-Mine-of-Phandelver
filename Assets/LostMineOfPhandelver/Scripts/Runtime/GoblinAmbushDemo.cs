using LostMineOfPhandelver.Core;
using UnityEngine;

namespace LostMineOfPhandelver.Runtime
{
    public sealed class GoblinAmbushDemo : MonoBehaviour
    {
        private readonly CharacterStats _hero = new CharacterStats(16, 13, 15, 10, 12, 8);
        private CheckResult? _lastCheck;
        private string _story = "На дороге к Фандалину лежат брошенные припасы. В кустах подозрительно тихо.";

        private GUIStyle _title;
        private GUIStyle _body;
        private GUIStyle _button;

        private void OnGUI()
        {
            EnsureStyles();
            float width = Mathf.Min(820f, Screen.width - 40f);
            GUILayout.BeginArea(new Rect((Screen.width - width) / 2f, 30f, width, Screen.height - 60f), GUI.skin.box);

            GUILayout.Label("LOST MINE OF PHANDELVER — ПРОТОТИП", _title);
            GUILayout.Space(12f);
            GUILayout.Label(
                $"Воин 1 уровня | СИЛ {_hero.Strength}  ЛОВ {_hero.Dexterity}  ТЕЛ {_hero.Constitution}  " +
                $"ИНТ {_hero.Intelligence}  МДР {_hero.Wisdom}  ХАР {_hero.Charisma}",
                _body);
            GUILayout.Space(20f);
            GUILayout.Label(_story, _body);
            GUILayout.Space(16f);

            if (!_lastCheck.HasValue && GUILayout.Button("[Мудрость (Восприятие), КС 15] Осмотреть дорогу", _button))
                ResolvePerceptionCheck();

            if (_lastCheck.HasValue && GUILayout.Button("Начать сцену заново", _button))
            {
                _lastCheck = null;
                _story = "На дороге к Фандалину лежат брошенные припасы. В кустах подозрительно тихо.";
            }

            if (_lastCheck.HasValue)
            {
                CheckResult result = _lastCheck.Value;
                GUILayout.Space(20f);
                GUILayout.Label(
                    $"d20: {result.Roll} {Format(result.AbilityModifier)} (Мудрость) " +
                    $"{Format(result.ProficiencyBonus)} (мастерство) = {result.Total}; КС {result.DifficultyClass}",
                    _body);
                GUILayout.Label(result.IsSuccess ? "Успех" : "Неудача", _body);
            }

            GUILayout.FlexibleSpace();
            GUILayout.Label("Техническая сцена: собственный код и тестовый пересказ без текста книги.", GUI.skin.label);
            GUILayout.EndArea();
        }

        private void ResolvePerceptionCheck()
        {
            var resolver = new AbilityCheckResolver(new RandomD20Roller());
            _lastCheck = resolver.Resolve(_hero.Wisdom, proficiencyBonus: 2, difficultyClass: 15);
            _story = _lastCheck.Value.IsSuccess
                ? "Герой замечает движение в зарослях. Отряд успевает подготовиться к гоблинской засаде."
                : "Стрела вылетает из кустов прежде, чем отряд понимает, что оказался в засаде.";
        }

        private void EnsureStyles()
        {
            if (_title != null) return;
            _title = new GUIStyle(GUI.skin.label) { fontSize = 24, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
            _body = new GUIStyle(GUI.skin.label) { fontSize = 17, wordWrap = true };
            _button = new GUIStyle(GUI.skin.button) { fontSize = 16, wordWrap = true, fixedHeight = 48f };
        }

        private static string Format(int value) => value >= 0 ? $"+ {value}" : $"- {-value}";
    }
}
