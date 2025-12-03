import { useState } from "react";

function MachinesTable({ headers, columns, machines }) {
  const [sortByColumns, setSortByColumns] = useState([]);

  const formattedMachines = machines.map((machine) => {
    const object = { machine, formatted: {} };
    for (const headerId of headers) {
      const column = columns[headerId];
      object.formatted[headerId] = (column.stringValue || column.value)(
        machine
      );
    }
    return object;
  });

  let sortedMachines = formattedMachines;
  for (const { key, sort } of sortByColumns) {
    sortedMachines = sortedMachines.sort((a, b) => {
      const aValue = a.formatted[key];
      const bValue = b.formatted[key];

      if (sort === "desc") return bValue.localeCompare(aValue);
      if (sort === "asc") return aValue.localeCompare(bValue);

      return 0;
    });
  }

  const setSortBy = (headerId) => {
    const currentSort = sortByColumns.find(({ key }) => key === headerId);
    if (!currentSort)
      return setSortByColumns([
        ...sortByColumns,
        { key: headerId, sort: "desc" },
      ]);

    const sortWithoutHeader = sortByColumns.filter(
      ({ key }) => key !== headerId
    );
    if (currentSort.sort === "asc") return setSortByColumns(sortWithoutHeader);

    return setSortByColumns([
      ...sortWithoutHeader,
      { key: headerId, sort: "asc" },
    ]);
  };

  return (
    <table className="machinesTable">
      <thead>
        <tr className="machinesTableHeaderContainer">
          {headers.map((headerId) => {
            const header = columns[headerId];
            const sort = sortByColumns.find(({ key }) => key === headerId);

            return (
              <th
                key={headerId}
                onClick={() => setSortBy(headerId)}
                className="machinesTableHeaderCell"
              >
                <span className="machinesTableHeaderText">{header.name}</span>
                <span className="machinesTableHeaderIcon">{sort ? sort.sort === "desc" ? "▼" : "▲" : "≡"}</span>
              </th>
            );
          })}
        </tr>
      </thead>
      <tbody>
        {sortedMachines.map(({ machine, formatted }) => {
          const statusColor = {
            Operational: "green",
            Broken: "red",
            InService: "yellow",
          }[machine.Status];

          return (
            <tr key={machine.Id} className="machinesTableBodyContainer">
              {headers.map((headerId) => {
                if (headerId === "Status")
                  return (
                    <th key={headerId} style={{ background: statusColor }}>
                      {formatted.Status}
                    </th>
                  );

                return <th key={headerId}>{formatted[headerId]}</th>;
              })}
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

export { MachinesTable };
