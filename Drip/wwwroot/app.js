window.toggleDarkMode = () => {
  if (document.documentElement.classList.toggle('dark')) {
    localStorage.setItem('theme', 'dark');
  } else {
    localStorage.setItem('theme', 'light');
  }
};

window.toggleSidebar = () => {
  const width = document.documentElement.clientWidth;
  
  if (width < 992) {
    document.documentElement.classList.toggle('sidebar-open-mobile');
  } else {
    document.documentElement.classList.toggle('sidebar-closed-desktop');
  }
};

// On load, set theme from localStorage
(function () {
  let theme = localStorage.getItem('theme');
  if (!theme) {
    // get preferred theme
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    theme = prefersDark ? 'dark' : 'light';
    localStorage.setItem('theme', theme);
  }
  if (theme === 'dark') {
    document.documentElement.classList.add('dark');
  }
})();