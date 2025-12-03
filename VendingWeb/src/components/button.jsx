function Button({ style = "primary", className, onClick, text }) {
  const styleClass = { primary: "buttonPrimary" }[style ?? "primary"];

  return <button className={`${styleClass} ${className || ""}`} onClick={onClick}>{text}</button>;
}

export { Button };
