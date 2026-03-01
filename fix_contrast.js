const fs = require('fs');
const path = require('path');

function walkDir(dir, callback) {
    fs.readdirSync(dir).forEach(f => {
        let dirPath = path.join(dir, f);
        let isDirectory = fs.statSync(dirPath).isDirectory();
        isDirectory ? walkDir(dirPath, callback) : callback(path.join(dir, f));
    });
}

function refactorFile(filePath) {
    if (!filePath.endsWith('.vue')) return;

    let content = fs.readFileSync(filePath, 'utf8');
    let newContent = content;

    // Fix buttons that have bg-blue-600 to have text-white instead of text-slate-900
    newContent = newContent.replace(/bg-blue-600([\s\S]*?)text-slate-900/g, 'bg-blue-600$1text-white');
    newContent = newContent.replace(/text-slate-900([\s\S]*?)bg-blue-600/g, 'text-white$1bg-blue-600');

    // One specific fix for UserDashboard.vue ("Nuevo" ticket button which now has hover:text-slate-900 on bg-blue-600 hover)
    newContent = newContent.replace(/hover:bg-blue-600 text-slate-500 hover:text-slate-900/g, 'hover:bg-blue-600 text-slate-500 hover:text-white');

    if (content !== newContent) {
        fs.writeFileSync(filePath, newContent, 'utf8');
        console.log(`Fixed contrast: ${filePath}`);
    }
}

const dir = path.join(__dirname, 'TicketSystem.Frontend', 'src');
walkDir(dir, refactorFile);
console.log('Contrast fix complete!');
