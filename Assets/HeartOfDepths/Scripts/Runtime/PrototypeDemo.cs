using HeartOfDepths.Core;
using UnityEngine;

namespace HeartOfDepths.Runtime
{
    public sealed class PrototypeDemo : MonoBehaviour
    {
        private readonly CharacterStats _rin = new CharacterStats(4, 2, 3, 4, 2);
        private readonly Integrity _integrity = new Integrity();
        private CheckResult? _lastCheck;
        private string _story = "Пленный Собиратель: «Экспедиция ещё жива. Пока они помнят свои имена».";

        private GUIStyle _title;
        private GUIStyle _body;
        private GUIStyle _button;

        private void OnGUI()
        {
            EnsureStyles();

            float width = Mathf.Min(760f, Screen.width - 40f);
            GUILayout.BeginArea(new Rect((Screen.width - width) / 2f, 30f, width, Screen.height - 60f), GUI.skin.box);

            GUILayout.Label("СЕРДЦЕ ГЛУБИН — ПРОТОТИП", _title);
            GUILayout.Space(12f);
            GUILayout.Label($"Рин  |  Мощь {_rin.Might}  Ловкость {_rin.Agility}  Разум {_rin.Intellect}  Воля {_rin.Will}  Влияние {_rin.Influence}", _body);
            GUILayout.Label($"Целостность: {_integrity.Value}/100", _body);
            GUILayout.Space(20f);
            GUILayout.Label(_story, _body);
            GUILayout.Space(16f);

            if (GUILayout.Button("[Воля 4] Прикоснуться к украденному воспоминанию", _button))
                ResolveMemoryCheck();

            if (GUILayout.Button("Отказаться от мнемантии", _button))
            {
                _lastCheck = null;
                _story = "Рин отступает. Мара одобряет осторожность, но след экспедиции остывает.";
            }

            if (_lastCheck.HasValue)
            {
                CheckResult result = _lastCheck.Value;
                GUILayout.Space(20f);
                GUILayout.Label($"Бросок: {result.FirstDie} + {result.SecondDie} {FormatModifier(result.Modifier)} = {result.Total}", _body);
                GUILayout.Label($"Исход: {Translate(result.Outcome)}", _body);
            }

            GUILayout.FlexibleSpace();
            GUILayout.Label("Техническая сцена: проверяем логику до художественного интерфейса.", GUI.skin.label);
            GUILayout.EndArea();
        }

        private void ResolveMemoryCheck()
        {
            var resolver = new CheckResolver(new RandomDiceRoller());
            _lastCheck = resolver.Resolve(_rin.Will, skill: 0, difficulty: 2);
            _integrity.Spend(10);

            _story = _lastCheck.Value.Outcome switch
            {
                CheckOutcome.FullSuccess => "Рин видит путь к шахте и лицо выжившего. Воспоминание остаётся целым.",
                CheckOutcome.SuccessWithCost => "Путь открыт, но чужой страх на несколько мгновений становится страхом Рина.",
                CheckOutcome.FailureWithProgress => "Рин узнаёт направление, однако Собиратель замечает вторжение.",
                _ => "Чужая память захлопывается. Рин забывает имя человека, которого когда-то не спас."
            };
        }

        private void EnsureStyles()
        {
            if (_title != null)
                return;

            _title = new GUIStyle(GUI.skin.label) { fontSize = 24, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
            _body = new GUIStyle(GUI.skin.label) { fontSize = 17, wordWrap = true };
            _button = new GUIStyle(GUI.skin.button) { fontSize = 16, wordWrap = true, fixedHeight = 48f };
        }

        private static string FormatModifier(int modifier) => modifier >= 0 ? $"+ {modifier}" : $"- {-modifier}";

        private static string Translate(CheckOutcome outcome) => outcome switch
        {
            CheckOutcome.FullSuccess => "полный успех",
            CheckOutcome.SuccessWithCost => "успех с ценой",
            CheckOutcome.FailureWithProgress => "неудача с продвижением",
            _ => "тяжёлая неудача"
        };
    }
}
