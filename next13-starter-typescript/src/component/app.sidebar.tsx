"use client";
import "@/styles/app.scss";
import React from "react";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import Dropdown from "react-bootstrap/Dropdown";
import DropdownButton from "react-bootstrap/DropdownButton";

function SidebarMenu() {
  const menu = [
    {
      name: "Dashboard",
      children: [],
    },
    {
      name: "Orders",
      children: [],
    },
    {
      name: "Menus",
      children: [],
    },
    {
      name: "Sales",
      children: [],
    },
    {
      name: "Notifications",
      children: [],
    },
    {
      name: "Analytics 123",
      children: [],
    },
  ];
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="bg-dark col-auto col-sm-2 d-flex flex-column justify-content-between min-vh-100">
          <div>
            <a
              className="ms-4 text-decoration-none text-white d-flex align-items-center"
              href="/"
            >
              <span className="ms-1 fs-4">TopSalad</span>
            </a>
            <hr></hr>
            <ul className="nav nav-pills flex-column">
              {menu.map((item) => (
                <li className="nav-item text-white my-1" key={item.name}>
                  <a className="nav-link" href="#" aria-current="page">
                    <i className="bi bi-speedometer2"></i>
                    <span className="ms-2">{item.name}</span>
                  </a>
                </li>
              ))}
            </ul>
          </div>
          <div>
            <DropdownButton
              id="dropdown-button"
              as={ButtonGroup}
              variant="success"
              menuVariant="dark"
              title="Phuong Uyen Ng"
            >
              <Dropdown.Item eventKey="1">Profile</Dropdown.Item>
              <Dropdown.Item eventKey="2">Setting</Dropdown.Item>
              <Dropdown.Divider />
              <Dropdown.Item eventKey="4">Log out</Dropdown.Item>
            </DropdownButton>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SidebarMenu;
