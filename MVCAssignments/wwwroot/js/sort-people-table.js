function sortTable(cellIndex) {
    let table = document.querySelector("table");
    let tableRows = table.rows;

    let haveRowsBeenSwitched = false;

    let currentRowCellToCompare;
    let nextRowCellToCompare;

    let sortDirection = "ascending";

    let switching;

    do {
        switching = false;

        for (let i = 1; i < (tableRows.length - 1); i++) {
            currentRowCellToCompare = tableRows[i].querySelectorAll("td")[cellIndex];
            nextRowCellToCompare = tableRows[i + 1].querySelectorAll("td")[cellIndex];

            if (sortDirection == "ascending") {
                if (currentRowCellToCompare.innerHTML.toLowerCase() > nextRowCellToCompare.innerHTML.toLowerCase()) {
                    tableRows[i].parentNode.insertBefore(tableRows[i + 1], tableRows[i]);
                    switching = true;
                    haveRowsBeenSwitched = true;
                    break;
                }
            } else if (sortDirection == "descending") {
                if (currentRowCellToCompare.innerHTML.toLowerCase() < nextRowCellToCompare.innerHTML.toLowerCase()) {
                    tableRows[i].parentNode.insertBefore(tableRows[i + 1], tableRows[i]);
                    switching = true;
                    haveRowsBeenSwitched = true;
                    break;
                }
            }
        }

        if (!haveRowsBeenSwitched && sortDirection == "ascending") {
            sortDirection = "descending";
            switching = true;
        }

    } while (switching)
}