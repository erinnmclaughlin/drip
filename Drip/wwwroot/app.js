window.toggleDarkMode = () => {
  document.documentElement.classList.toggle('dark');
};

window.toggleSidebar = () => {
  const width = document.documentElement.clientWidth;
  
  if (width < 992) {
    document.documentElement.classList.toggle('sidebar-open-mobile');
  } else {
    document.documentElement.classList.toggle('sidebar-closed-desktop');
  }
};
