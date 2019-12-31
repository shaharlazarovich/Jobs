import React from 'react'
import { Dimmer, Loader } from 'semantic-ui-react'

//in this component, instead of declaring a class as its type
//and pass properties that way - we are declaring the object inline
//we add the question mark to the properties to indicate they are
//optional
const LoadingComponent: React.FC<{inverted?: boolean,content?: string}> = ({
    inverted=true,
    content
}) => {
    return (
      <Dimmer active inverted={inverted}>
        <Loader content={content} />
      </Dimmer>
    )
}

export default LoadingComponent
