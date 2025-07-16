#!/bin/bash
echo "🚀 Starting SVM Studio with Authentication & Role Management..."
echo "📍 Project directory: /Users/Nanyu/Documents/✨Personal projects/SVMStudio"
echo ""

cd "/Users/Nanyu/Documents/✨Personal projects/SVMStudio"

echo "🔧 Building project..."
dotnet build

if [ $? -eq 0 ]; then
    echo "✅ Build successful!"
    echo ""
    echo "🌐 Starting application..."
    echo "📖 Sample accounts:"
    echo "   Admin: admin@svmstudio.com / Admin123!"
    echo "   Staff: staff@svmstudio.com / Staff123!"
    echo "   User:  user@svmstudio.com / User123!"
    echo ""
    echo "🔗 URLs:"
    echo "   HTTP:  http://localhost:8080"
    echo "   HTTPS: https://localhost:8081"
    echo "   Auth Test: http://localhost:8080/Home/AuthTest"
    echo ""
    echo "🔧 Debug: Add ?debug=true to URL to see auth state"
    echo ""
    echo "Press Ctrl+C to stop the server"
    echo "======================================"
    
    dotnet run
else
    echo "❌ Build failed!"
fi
