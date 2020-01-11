import React from 'react'
import {Tab} from 'semantic-ui-react'
import ProfileDescription from './ProfileDescription'

interface IProps {
    setActiveTab: (activeIndex: any) => void;
}

const panes = [
    {menuItem: 'About', render: () => <ProfileDescription />}
]
const ProfileContent: React.FC<IProps> = ({setActiveTab}) => {
    return (
        <Tab 
            data-test="component-profile-content"
            menu={{fluid: true, vertical: true}}
            menuPosition='right'
            panes={panes}
            onTabChange={(e, data) => setActiveTab(data.activeIndex)}
        />
    )
}

export default ProfileContent;
