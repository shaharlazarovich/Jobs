import React, { useContext, Fragment } from 'react'
import { Container, Segment, Header, Button, Image } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { RootStoreContext } from '../../app/stores/rootStore'
import LoginForm from '../user/LoginForm'
import RegisterForm from '../user/RegisterForm'

const HomePage = () => {
    const token = window.localStorage.getItem('jwt');
    const rootStore = useContext(RootStoreContext);
    const {isLoggedIn, user} = rootStore.userStore;
    const {openModal} = rootStore.modalStore;
    return (
    <Segment inverted textAlign='center' vertical className='masthead' >
        <Container text>
            <Header as='h1' inverted>
                <Image size='massive' src='/assets/logo.png' alt='logo' style={{marginBottom: 12}}/>
                Mini Console
            </Header>
            {isLoggedIn && user && token ? ( //we will display the welcome back message if the user is logged in and a token exists (didn't expire)
                <Fragment>
                    <Header as='h2' inverted content={`Welcome back ${user.displayName}`} />
                    <Button as={Link} to='/jobs' size='huge' inverted>
                    Go to Jobs!
                    </Button>
                </Fragment>    
            ) : (
                <Fragment>
                    <Header as='h2' inverted content='Welcome to Miniconsole' />
                    <Button onClick={() => openModal(<LoginForm />)} size='huge' inverted>
                         Login
                    </Button>
                    <Button onClick={() => openModal(<RegisterForm />)} size='huge' inverted>
                         Register
                    </Button>
                </Fragment>
            )}
        </Container>
    </Segment>
    )
}

export default HomePage
