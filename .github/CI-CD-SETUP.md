# CI/CD Setup Guide

## 🔧 GitHub Actions Workflows

This repository includes automated code quality and review workflows:

### 1. SonarQube Analysis
- **Trigger**: Push to `main`/`development` or Pull Requests
- **Purpose**: Static code analysis, code coverage, security vulnerabilities
- **File**: `.github/workflows/sonarqube.yml`

### 2. Gemini AI Code Review
- **Trigger**: Pull Requests (opened, synchronized, reopened)
- **Purpose**: AI-powered code review with suggestions
- **File**: `.github/workflows/gemini-code-review.yml`

---

## 🔑 Required Secrets

You need to configure the following secrets in GitHub:
**Settings → Secrets and variables → Actions → New repository secret**

### For SonarQube:
1. **SONAR_TOKEN**: Your SonarQube authentication token
   - Get from: SonarQube → My Account → Security → Generate Token
   
2. **SONAR_HOST_URL**: Your SonarQube server URL
   - SonarCloud: `https://sonarcloud.io`
   - Self-hosted: Your server URL (e.g., `https://sonar.yourcompany.com`)
   
3. **SONAR_PROJECT_KEY**: Your project key in SonarQube
   - Example: `MmdNzhd_Verifier`

### For Gemini AI:
4. **GEMINI_API_KEY**: Your Google Gemini API key
   - Get from: https://makersuite.google.com/app/apikey
   - Or: https://aistudio.google.com/app/apikey

---

## 📋 Setup Instructions

### Step 1: SonarQube Setup

#### Option A: Use SonarCloud (Free for public repos)
1. Go to https://sonarcloud.io
2. Sign in with GitHub
3. Click "+" → "Analyze new project"
4. Select "Verifier" repository
5. Choose "With GitHub Actions"
6. Copy the provided `SONAR_TOKEN` and `SONAR_PROJECT_KEY`
7. Add secrets to GitHub (see above)
8. Set `SONAR_HOST_URL` to `https://sonarcloud.io`

#### Option B: Self-hosted SonarQube
1. Install SonarQube on your server
2. Create a new project
3. Generate an authentication token
4. Add secrets to GitHub with your server URL

### Step 2: Gemini API Setup

1. Go to https://aistudio.google.com/app/apikey
2. Click "Create API Key"
3. Copy the API key
4. Add as `GEMINI_API_KEY` secret in GitHub

### Step 3: Add Secrets to GitHub

1. Go to: https://github.com/MmdNzhd/Verifier/settings/secrets/actions
2. Click "New repository secret"
3. Add each secret:
   - Name: `SONAR_TOKEN`, Value: [your token]
   - Name: `SONAR_HOST_URL`, Value: [your URL]
   - Name: `SONAR_PROJECT_KEY`, Value: [your project key]
   - Name: `GEMINI_API_KEY`, Value: [your API key]

---

## ✅ Testing the Setup

### Test SonarQube:
1. Push a commit to `development` branch
2. Go to: https://github.com/MmdNzhd/Verifier/actions
3. Check the "SonarQube Analysis" workflow
4. View results in SonarQube dashboard

### Test Gemini Review:
1. Create a Pull Request
2. Gemini will automatically comment with code review
3. Check the PR comments for AI suggestions

---

## 🎯 Workflow Behavior

### SonarQube runs on:
- Every push to `main` or `development`
- Every Pull Request
- Provides: Code smells, bugs, vulnerabilities, code coverage

### Gemini runs on:
- Every Pull Request (when opened or updated)
- Reviews only `.cs` and `.csproj` files
- Provides: Security, quality, performance, and bug suggestions

---

## 📊 Expected Results

After setup, every PR will have:
1. ✅ SonarQube quality gate status
2. 🤖 Gemini AI code review comment
3. Automated feedback before you review manually

---

## 🔒 Security Notes

- All API keys are stored as GitHub Secrets (encrypted)
- Secrets are never exposed in logs
- Only repository admins can view/edit secrets
- Workflows run in isolated environments

---

## 🆘 Troubleshooting

### SonarQube fails:
- Check `SONAR_TOKEN` is valid
- Verify `SONAR_HOST_URL` is correct
- Ensure project exists in SonarQube

### Gemini fails:
- Check `GEMINI_API_KEY` is valid
- Verify API key has not exceeded quota
- Check workflow logs for specific errors

---

## 📝 Next Steps

1. Add the required secrets (see above)
2. Create a test PR to verify both workflows
3. Check SonarQube dashboard for initial analysis
4. Review Gemini's AI suggestions on PRs

