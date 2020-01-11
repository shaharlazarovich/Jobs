import React, { useContext } from 'react'
import { Menu, Container, Button, Dropdown, Image } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite';
import { NavLink, Link } from 'react-router-dom';
import { RootStoreContext } from '../../app/stores/rootStore';

const NavBar: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { user, logout} = rootStore.userStore;
    return (
        //we are using the keyword "as" to transform the component  
        //Menu.Item or Button into a Router Link while keeping the Menu.Item style.
        <Menu data-test="component-navbar" fixed='top' inverted>
            <Container>
                <Menu.Item header as={NavLink} exact to='/' > 
                    <img src="/assets/logo.png" alt="logo" style={{marginRight:'10px'}} />
                    EDRM
                </Menu.Item>
                <Menu.Item name='Jobs' as={NavLink} to='/jobs' />
                <Menu.Item>
                    <Button 
                        //onClick={openCreateForm} //i'm keeping this to show how it was before the mobx refactoring
                        //onClick={activityStore.openCreateForm}i'm keeping this to show state before router refactoring
                        as={NavLink} to="/createjob"
                        positive 
                        content='Create Job' />
                </Menu.Item>
                {user &&  
                    <Menu.Item position='right'>
                          <Image avatar spaced='right' src={user.image || '/assets/user.png'} />
                          <Dropdown pointing='top left' text={user.displayName}>
                            <Dropdown.Menu>
                              <Dropdown.Item as={Link} to={`/profile/${user.username}`} text='My profile' icon='user'/>
                              <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                            </Dropdown.Menu>
                          </Dropdown>
                    </Menu.Item>

                }
            </Container>
      </Menu>
    )
}

//export default NavBar
export default observer(NavBar)
