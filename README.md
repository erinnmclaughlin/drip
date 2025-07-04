# Drip

A Blazor WebAssembly application for API documentation and testing.

## GitHub Pages Deployment

This project is configured to automatically deploy to GitHub Pages using GitHub Actions.

### Setup Instructions

1. **Enable GitHub Pages in your repository:**
   - Go to your repository on GitHub
   - Navigate to Settings → Pages
   - Under "Source", select "GitHub Actions"
   - This will allow the workflow to deploy your site

2. **Push your code:**
   - The workflow will automatically trigger when you push to the `main` branch
   - You can also manually trigger it from the Actions tab

3. **Access your site:**
   - Your site will be available at `https://[your-username].github.io/[repository-name]/`
   - The first deployment may take a few minutes

### How it works

The GitHub Actions workflow:
1. Builds your Blazor WebAssembly application
2. Publishes the static files
3. Deploys them to GitHub Pages

The site includes:
- `.nojekyll` file to prevent Jekyll processing
- `404.html` for client-side routing support
- Updated `index.html` with routing redirect handling

### Local Development

To run the project locally:

```bash
cd Drip
dotnet run
```

The application will be available at `https://localhost:5001` (or the port shown in the console). 