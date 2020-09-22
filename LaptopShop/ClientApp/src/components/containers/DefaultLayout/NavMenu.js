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
  constructor (props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.state = {
      isOpen: false
    };
  }
  toggle () {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light >
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
                  <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark" to="/products/notebooks">Products</NavLink>
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
                  <NavLink activeClassName="active" tag={Link} className="nav-link text-dark" to="/register">Реєстрація</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink activeClassName="active"  tag={Link} className="nav-link text-dark" to="/login">Логін</NavLink>
                </NavItem>
              </ul>
              }
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

const mapState=(state)=>{
  return {
    isAuthenticated: state.auth.isAuthenticated
  }
}

export default connect(mapState,{logout})(NavMenu)