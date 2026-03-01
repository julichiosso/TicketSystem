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

    // Fix structural text-white
    newContent = newContent.replace(/<div class="min-h-screen text-white/g, '<div class="min-h-screen text-slate-900');
    newContent = newContent.replace(/text-xl font-black text-white/g, 'text-xl font-black text-slate-900');
    newContent = newContent.replace(/text-lg font-black text-white/g, 'text-lg font-black text-slate-900');

    // Fix hover:text-white on light backgrounds
    newContent = newContent.replace(/hover:text-white hover:bg-slate-50/g, 'hover:text-slate-900 hover:bg-slate-50');
    newContent = newContent.replace(/hover:text-white hover:bg-white/g, 'hover:text-slate-900 hover:bg-white');
    newContent = newContent.replace(/text-slate-500 hover:text-white/g, 'text-slate-500 hover:text-slate-900');

    // Fix ChatBox white-on-white
    newContent = newContent.replace(/'bg-white text-white/g, "'bg-white text-slate-900");

    if (content !== newContent) {
        fs.writeFileSync(filePath, newContent, 'utf8');
        console.log(`Fixed contrast 2: ${filePath}`);
    }
}

const dir = path.join(__dirname, 'TicketSystem.Frontend', 'src');
walkDir(dir, refactorFile);
console.log('Contrast fix 2 complete!');
