// ===================== THEME =====================
(function () {
    const themes = [
        { value: "light", label: " Light" },
        { value: "dark", label: " Dark" },
        { value: "blue", label: " Blue" },
        { value: "purple", label: " Purple" },
        { value: "navy", label: " Navy" },
        { value: "graymetal", label: " Gray Metal" },
        { value: "green", label: " Green Dark" },
        { value: "deepblue", label: " Deep Blue" },
        { value: "chocolate", label: " Chocolate" },
        { value: "trueblack", label: " Pure Black" },
        { value: "royalpurple", label: " Royal Purple" },
        { value: "hotred", label: "Hot Red" },
        { value: "cyanocean", label: "Cyan Ocean" },
        { value: "darkpink", label: "Dark Pink" }
    ];

    const STORAGE_KEY = "theme";
    const themeSelector = document.getElementById("themeSelector");
    const savedTheme = localStorage.getItem(STORAGE_KEY) || "light";

    function removeThemeClasses() {
        const toRemove = [];
        document.body.classList.forEach(cls => {
            if (cls.startsWith("theme-")) toRemove.push(cls);
        });
        toRemove.forEach(cls => document.body.classList.remove(cls));
    }

    function applyTheme(themeValue) {
        removeThemeClasses();
        document.body.classList.add("theme-" + themeValue);
        localStorage.setItem(STORAGE_KEY, themeValue);
        document.dispatchEvent(new CustomEvent("theme:changed", { detail: { theme: themeValue } }));
    }

    if (themeSelector) {
        themeSelector.innerHTML = "";
        themes.forEach(t => {
            const opt = document.createElement("option");
            opt.value = t.value;
            opt.textContent = t.label;
            themeSelector.appendChild(opt);
        });

        themeSelector.value = savedTheme;
        themeSelector.addEventListener("change", () => applyTheme(themeSelector.value));
    }

    applyTheme(savedTheme);
})();


// ===================== CLOCK =====================
(function () {
    const clockStyles = [
        "classic", "monochrome", "bright", "transparent", "large",
        "small", "elegant", "sharp", "futuristic", "encrypted"
    ];

    const CLOCK_KEY = "clockStyle";
    const clock = document.getElementById("clock");
    const clockStyleSelector = document.getElementById("clockStyleSelector");
    const savedClockStyle = localStorage.getItem(CLOCK_KEY) || "classic";

    if (!clock) return; 

    if (clockStyleSelector) {
        clockStyleSelector.innerHTML = "";
        clockStyles.forEach(style => {
            const opt = document.createElement("option");
            opt.value = style;
            opt.textContent = style.charAt(0).toUpperCase() + style.slice(1);
            clockStyleSelector.appendChild(opt);
        });
        clockStyleSelector.value = savedClockStyle;

        clockStyleSelector.addEventListener("change", () => {
            applyClockStyle(clockStyleSelector.value);
            localStorage.setItem(CLOCK_KEY, clockStyleSelector.value);
        });
    }

    function updateClock() {
        const now = new Date();
        const options = {
            weekday: "long", year: "numeric", month: "short", day: "numeric",
            hour: "2-digit", minute: "2-digit", second: "2-digit"
        };
        clock.textContent = now.toLocaleString("en-US", options);
    }

    function applyClockStyle(style) {
        clock.className = "clock-" + style + " clock-ring";
    }

    applyClockStyle(savedClockStyle);
    updateClock();
    setInterval(updateClock, 1000);
})();
