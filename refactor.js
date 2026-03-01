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

    // Global search & replace tailored for Tailwind utilities in this Vue app
    const replacements = [
        // Backgrounds
        { search: /bg-slate-950/g, replace: 'bg-slate-50' },
        { search: /bg-slate-900\/[0-9]+/g, replace: 'bg-white' },
        { search: /bg-slate-900/g, replace: 'bg-white' },
        { search: /bg-slate-800\/[0-9]+/g, replace: 'bg-white' },
        { search: /bg-slate-800/g, replace: 'bg-white' },
        // Borders
        { search: /border-slate-800/g, replace: 'border-slate-200' },
        { search: /border-slate-700/g, replace: 'border-slate-200' },
        { search: /border-white\/[0-9]+/g, replace: 'border-slate-200' },
        { search: /border-white\/5/g, replace: 'border-slate-200' },
        // Text colors
        { search: /text-white/g, replace: 'text-slate-900' },
        { search: /text-slate-200/g, replace: 'text-slate-900' },
        { search: /text-slate-300/g, replace: 'text-slate-700' },
        { search: /text-slate-400/g, replace: 'text-slate-500' },
        // Structural & aesthetic
        { search: /premium-blur/g, replace: 'bg-white border border-slate-200 shadow-sm' },
        { search: /backdrop-blur-[a-z0-9]+/g, replace: '' },
        { search: /blur-[a-z0-9]+/g, replace: '' },
        // Hover styles
        { search: /hover:bg-white\/[0-9]+/g, replace: 'hover:bg-slate-50' },
        { search: /hover:text-white/g, replace: 'hover:text-slate-900' },
        { search: /group-hover:border-blue-500\/30/g, replace: 'group-hover:border-blue-300' },
        { search: /shadow-blue-500\/[0-9]+/g, replace: 'shadow-blue-200' },
        { search: /shadow-2xl/g, replace: 'shadow-md' },
        { search: /shadow-xl/g, replace: 'shadow-sm' },
        // Input placeholder adjustments
        { search: /placeholder:text-slate-700/g, replace: 'placeholder:text-slate-400' },
        { search: /placeholder-slate-500/g, replace: 'placeholder-slate-400' },
        // Outfit font references explicitly used (if any)
        { search: /font-\['Outfit'\]/g, replace: 'font-sans' }
    ];

    replacements.forEach(({ search, replace }) => {
        newContent = newContent.replace(search, replace);
    });

    // Multiple spaces cleanup
    newContent = newContent.replace(/  +/g, ' ');
    // Clean empty class attributes left over
    newContent = newContent.replace(/class="\s+"/g, '');

    if (content !== newContent) {
        fs.writeFileSync(filePath, newContent, 'utf8');
        console.log(`Refactored: ${filePath}`);
    }
}

const dir = path.join(__dirname, 'TicketSystem.Frontend', 'src');
walkDir(dir, refactorFile);
console.log('Refactor complete!');
