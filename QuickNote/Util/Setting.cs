using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNote.Util
{
    public class Setting
    {
        public string[] TagButtonNameList { get; set; } =
            {
            "Python",
            "C++",
            "Golang",
            "JavaScript",
            "Java",
            "HTML",
            "CSS",
            "C#",
            "TypeScript",
            "Node.js",
            "React",
            "Vue",
            "プログラミング",
            "日記",
            "音楽理論",
            "音楽",
            "料理",
            "豆知識",
            "ガジェット",
            "アイデア",
            "話したい話題",
            "経営",
            "AI",
            "Web3.0",
            "WPF",
            "楽曲分析",
            "YouTube",
            "niconico",
            "UStream",
            "VTuber",
            "LLM",
            "Unity",
            "UnrealEngin",
            "ポケモン",
            "英会話",
            "基本情報",
            "応用情報",
            "ネットワークスペシャリスト",
            "APEX",
            "Twitter",
            "競技プログラミング",
            "Qiita",
            "プロット",
            "謎解き",
            "プロンプト",
            "学習",
            "怖い",
        };

        public string LastSelectedFormat = "CaptureWeb";
        public string DefaultMode = "Writer";
    }
}
