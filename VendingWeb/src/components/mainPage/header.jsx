"use client";

import { UserContext } from "@/context";
import { useRouter } from "next/navigation";
import React, { useContext } from "react";

function Header() {
  const router = useRouter();
  const { openedPage, setOpenPage } = useContext(UserContext);

  const pages = [
    { name: "Торговые аппараты", id: "machines" },
    { name: "Календарь обслуживания", id: "maintenanceCalendar" },
    { name: "График работ", id: "workCalendar" },
  ];

  const onSelectPage = (selectedPageId) => {
    setOpenPage(selectedPageId);

    router.push(`/${selectedPageId}`);
  };

  return (
    <div className="headerContainer">
      {...pages.map((page) => {
        return (
          <HeaderChild
            id={page.id}
            name={page.name}
            isSelected={openedPage === page.id}
            onSelect={onSelectPage}
          />
        );
      })}

      <div className="accountHeaderChild"></div>
    </div>
  );
}

function HeaderChild({ id, name, isSelected, onSelect }) {
  const onSelectPage = () => {
    if (isSelected) return;

    onSelect(id);
  };

  const classes = ["headerChild"];
  if (isSelected) classes.push("selectedHeaderChild");

  return (
    <div className={classes.join(" ")} onClick={onSelectPage}>
      <span>{name}</span>
    </div>
  );
}

export { Header, HeaderChild };
