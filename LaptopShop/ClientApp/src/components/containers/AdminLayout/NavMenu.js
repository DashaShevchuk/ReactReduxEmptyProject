import React from 'react';
import { connect } from 'react-redux';
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from 'reactstrap';

import { logout } from '../../auth/Login/actions';

import { Link, NavLink } from 'react-router-dom';
import './NavMenu.scss';


export class NavMenu extends React.Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }
  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render() {
    return (
      <header>

        {/* <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
          <a class="navbar-brand" href="/">Буратіно</a>
          <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div class="navbar-nav">
              <a class="nav-item nav-link" href="/">Мальвіна</a>
              <a class="nav-item nav-link active" href="/gallery.html">Галерея <span class="sr-only">(current)</span></a>

            </div>
          </div>
          <div class="navbar-nav">
            <a class="nav-item nav-link" href="/login.html">Вхід</a>
            <a class="nav-item nav-link" href="/register.html">Реєстрація</a>
          </div>
        </nav> */}
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
          <Container>
            <NavbarBrand tag={Link} to="/">Laptop Shop</NavbarBrand>
            <NavbarToggler onClick={this.toggle} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
              {this.props.isAuthenticated ?
               <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark" exact={true} to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark" to="/counter">Counter</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark" to="/fetch-data">Fetch data</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark" to="/products">Products</NavLink>
                </NavItem>
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret className="text-dark">
                    Моє меню
                  </DropdownToggle>
                  <DropdownMenu right>
                    <DropdownItem>
                      <NavItem>
                        <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark p-0" to="/myprofile">Моє меню</NavLink>
                      </NavItem>
                    </DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem onClick={()=>this.props.logout()}>
                      Вихід
                    </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>
                
              </ul>
              : 
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink activeClassName="active" tag={Link} className="nav-link" to="/register">Реєстрація</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink activeClassName="active"  tag={Link} className="nav-link" to="/login">Логін</NavLink>
                </NavItem>
              </ul>
              }
            </Collapse>
          </Container>
        </nav>
      </header>
    );
  }
}

const mapState = (state) => {
  return {
    isAuthenticated: state.auth.isAuthenticated
  }
}

export default connect(mapState, { logout })(NavMenu)